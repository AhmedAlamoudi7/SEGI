$(document).ready(function () {
    // Initially load page number=1
    GetPageData(1);

    // Attach a click event listener to the submit button
    $("#searchForm").click(function (e) {
        e.preventDefault(); // Prevent the default form submission behavior
        performSearch();
    });


});

function GetPageData(pageNum, pageSize) {
    // After every trigger, remove previous data and paging
    $("#scriptList").empty();
    $("#paged").empty();

    $.getJSON("/script/scriptList", { pageNumber: pageNum, pageSize: pageSize, categoryId: $('#categoryId').val() }, function (response) {
        var rowData = "";
        for (var i = 0; i < response.data.length; i++) {
            // Updated to handle multiple categories
            var categories = response.data[i].categories.join(", ");

            rowData += `
                <div class="coach-item style-two wow fadeInUp delay-0-2s" id="coach-item-script">
                    <a class="coach-image" href="/Script/Detailes/${response.data[i].id}">
                        <!-- Use Lozad.js for lazy loading -->
                        <img data-src="/Files/Images/${response.data[i].image}" alt="Coach" class="img-fluid lozad">
                    </a>
                    <div class="row">
                        <h4 id="category_name">
                            <a href="/Script/Detailes/${response.data[i].id}">${response.data[i].name}</a>
                        </h4>
                        <h4 style="margin-right: -6% !important;">
                            <a href="/Script/Detailes/${response.data[i].id}" style="color: var(--Colors-Primary, #15B5DB);">${response.data[i].price}$</a>
                        </h4>
                    </div>
                    <div class="categories">
                        <strong>Categories: </strong>${categories}
                    </div>
                    <button data-script-id="${response.data[i].id}" id="Buy_now" class="theme-btn my-15 pay-script-btn">Buy Now</button>
                </div>`;
        }
        $("#scriptList").append(rowData);

        // Initialize Lozad.js for lazy-loaded images
        const observer = lozad('.lozad', {
            rootMargin: '50px 0px', // Load slightly before images enter the viewport
            threshold: 0.1, // Load when 10% of the image is visible
            loaded: function (el) {
                el.classList.add('loaded'); // Optional: Add a class after the image is loaded
            }
        });
        observer.observe();

        // Append pagination
        PaggingTemplate(response.totalPages, response.currentPage);
    });
}
// Category Filter
function theFunction(categoryId, pageNum, pageSize) {
    // Clear existing content
    $("#scriptList").empty();
    $("#paged").empty();

    $.getJSON("/script/scriptList?categoryId=" + categoryId, { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        let rowData = "";

        for (let i = 0; i < response.data.length; i++) {
            // Handle multiple categories
            const categories = response.data[i].categories.join(", ");

            rowData += `
                <div class="coach-item style-two wow fadeInUp delay-0-2s" id="coach-item-script">
                    <a class="coach-image" href="/Script/Detailes/${response.data[i].id}">
                        <!-- Use Lozad.js for lazy loading -->
                        <img data-src="/Files/Images/${response.data[i].image}" alt="Coach" class="img-fluid lozad">
                    </a>
                    <div class="row">
                        <h4 id="category_name">
                            <a href="/Script/Detailes/${response.data[i].id}">${response.data[i].name}</a>
                        </h4>
                        <h4 style="margin-right: -6% !important;">
                            <a href="/Script/Detailes/${response.data[i].id}" style="color: var(--Colors-Primary, #15B5DB);">${response.data[i].price}$</a>
                        </h4>
                    </div>
                    <div class="categories">
                        <strong>Categories: </strong>${categories}
                    </div>
                    <button data-script-id="${response.data[i].id}" id="Buy_now" class="theme-btn my-15 pay-script-btn">Buy Now</button>
                </div>`;
        }

        // Append the new rows to the DOM
        $("#scriptList").append(rowData);

        // Initialize Lozad.js for lazy-loaded images
        const observer = lozad('.lozad', {
            rootMargin: '50px 0px', // Load slightly before the image is visible
            threshold: 0.1, // Load when 10% of the element is visible
            loaded: function (el) {
                el.classList.add('loaded'); // Optional: Add a 'loaded' class for styling
            }
        });
        observer.observe();

        // Append pagination
        PaggingTemplate(response.totalPages, response.currentPage);
    });
}

function performSearch() {
    event.preventDefault(); // Prevent the form from submitting the default way

    // Prepare the data to be sent to the server
    var requestData = {
        searchInput: $('#kt_filter_search').val(),
        searchModelType: 3, // Replace 'YourEnumValue' with the appropriate enum value; 3 means Script
    };

    // Store the search input before loading the data
    $.ajax({
        url: '/ProfileAccount/AccountProfileUsers/StoreSearchInput', // Endpoint to store search input
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            console.log('Search input stored successfully.');
        },
        error: function (xhr, status, error) {
            console.log('Error storing search input: ' + error);
        }
    });

    // Clear existing content
    $("#scriptList").empty();
    $("#paged").empty();

    // Fetch data based on search input
    $.getJSON("/script/scriptList?GeneralSearch=" + $('#kt_filter_search').val(), { categoryId: $("#categoryId").val() }, function (response) {
        var rowData = "";

        for (var i = 0; i < response.data.length; i++) {
            // Handle multiple categories
            var categories = response.data[i].categories.join(", ");

            rowData += `
                <div class="coach-item style-two wow fadeInUp delay-0-2s" id="coach-item-script">
                    <a class="coach-image" href="/Script/Detailes/${response.data[i].id}">
                        <!-- Use Lozad.js for lazy loading -->
                        <img data-src="/Files/Images/${response.data[i].image}" alt="Coach" class="img-fluid lozad">
                    </a>
                    <div class="row">
                        <h4 id="category_name">
                            <a href="/Script/Detailes/${response.data[i].id}">${response.data[i].name}</a>
                        </h4>
                        <h4 style="margin-right: -6% !important;">
                            <a href="/Script/Detailes/${response.data[i].id}" style="color: var(--Colors-Primary, #15B5DB);">${response.data[i].price}$</a>
                        </h4>
                    </div>
                    <div class="categories">
                        <strong>Categories: </strong>${categories}
                    </div>
                    <button data-script-id="${response.data[i].id}" id="Buy_now" class="theme-btn my-15 pay-script-btn">Buy Now</button>
                </div>`;
        }

        // Append the new rows to the DOM
        $("#scriptList").append(rowData);

        // Initialize Lozad.js for lazy-loaded images
        const observer = lozad('.lozad', {
            rootMargin: '50px 0px', // Load slightly before images enter the viewport
            threshold: 0.1, // Load when 10% of the element is visible
            loaded: function (el) {
                el.classList.add('loaded'); // Optional: Add a class to indicate the image has been loaded
            }
        });
        observer.observe();

        // Append pagination
        PaggingTemplate(response.totalPages, response.currentPage);
    });
}

//This is paging temlpate ,you should just copy paste
function PaggingTemplate(totalPage, currentPage) {
    var template = "";
    var TotalPages = totalPage;
    var CurrentPage = currentPage;
    var PageNumberArray = Array();


    var countIncr = 1;
    for (var i = currentPage; i <= totalPage; i++) {
        PageNumberArray[0] = currentPage;
        if (totalPage != currentPage && PageNumberArray[countIncr - 1] != totalPage) {
            PageNumberArray[countIncr] = i + 1;
        }
        countIncr++;
    };
    PageNumberArray = PageNumberArray.slice(0, 5);
    var FirstPage = 1;
    var LastPage = totalPage;
    if (totalPage != currentPage) {
        var ForwardOne = currentPage + 1;
    }
    var BackwardOne = 1;
    if (currentPage > 1) {
        BackwardOne = currentPage - 1;
    }
    $("#paged").empty();
    template = template + '<ul class="pagination flex-wrap mt-20">' +
        '<li class="page-item disabled prev"><a href="#" class="page-link" href="#" rel="prev" aria-label="Prev &raquo;" onclick="GetPageData(' + BackwardOne + ')"><span class="page-link"><i class="fas fa-angle-double-left"></i></span></a></li>';

    var numberingLoop = "";
    if (PageNumberArray.length == 0) {
        numberingLoop = numberingLoop + '<div class="alert alert-warning" role="alert">No Data Found !</div> ';
    }
    for (var i = 0; i < PageNumberArray.length; i++) {


        if (PageNumberArray[i] == CurrentPage) {
            numberingLoop = numberingLoop + ' <li class="page-item active" aria-current="page"><a class="page-link" onclick="GetPageData(' + PageNumberArray[i] + ')" href="#">' + PageNumberArray[i] + '<span class="sr-only">(current)</span></a></li>'
        } else {
            numberingLoop = numberingLoop + ' <li class="page-item" aria-current="page"><a class="page-link" onclick="GetPageData(' + PageNumberArray[i] + ')" href="#">' + PageNumberArray[i] + ' <span class="sr-only">(current)</span></a></li>'
        }
        numberingLoop;
    }
    template = template + numberingLoop + '<li class="page-item next"><a class="page-link"style="text-align:center;" href="#" rel="next" aria-label="Next &raquo;" onclick="GetPageData(' + ForwardOne + ')"><i class="fas fa-angle-double-right"></i></a></li>';
    $("#paged").append(template);
    // Add click event for the Buy Now button
    $('.pay-script-btn').on('click', function (e) {
        e.preventDefault();

        // Show a SweetAlert confirmation dialog with a balance note
        Swal.fire({
            title: 'Checkout Purchase',
            html: `
            <div style="text-align: left;">
                <p>You are about to complete your purchase with the selected items. Please review your selections and ensure you have sufficient funds to proceed.</p>
                <p style="color: #e74c3c; font-weight: bold; margin-top: 10px;">Important: If your balance is insufficient, you will be redirected to the recharge page to top up your account.</p>
            </div>
        `,
            icon: 'info',
            showCancelButton: true,
            confirmButtonText: 'Yes, buy now!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // Get the script ID from the data attribute
                var scriptId = $(this).data('script-id');

                // Proceed with the AJAX request if confirmed
                $.ajax({
                    type: 'POST',
                    url: '/ProfileAccount/AccountProfileFinance/PurchaseScript/',
                    data: { id: scriptId },
                    success: function (response) {
                        if (response.success) {
                            // Redirect to the specified link if purchase is successful
                            window.location.href = response.link;
                        } else if (response.link === "/ProfileAccount/AccountProfileFinance/ChargeBalance") {
                            // Redirect if balance is insufficient
                            Swal.fire({
                                title: 'Insufficient Balance',
                                text: 'Your balance is insufficient. You will be redirected to charge your balance.',
                                icon: 'warning',
                                confirmButtonText: 'Charge Balance'
                            }).then(() => {
                                window.location.href = response.link;
                            });
                        } else {
                            // Handle other failure cases
                            Swal.fire('Failed', 'Purchase failed or was canceled. Redirecting...', 'error').then(() => {
                                window.location.href = response.link;
                            });
                        }
                    },
                    error: function () {
                        // Handle AJAX request errors
                        Swal.fire('Error', 'An error occurred. Please try again later.', 'error');
                    }
                });
            }
        });
    });
}

