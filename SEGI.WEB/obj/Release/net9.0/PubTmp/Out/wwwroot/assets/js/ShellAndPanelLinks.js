$(document).ready(function () {
    // Initially show the table and disable the "View Selected" button
    $("#scriptList").show();
    $("#viewSelected").prop('disabled', true);

    // Load all items on page load
    GetPageData(1, 100); // Change the second parameter to your desired page size

    // Handle Select All checkbox
    $(document).on('change', '#selectAll', function () {
        var isChecked = $(this).is(':checked');
        $("input[name='itemCheckbox']").prop('checked', isChecked);
        toggleViewSelectedButton();
    });

    // Handle individual checkbox changes
    $(document).on('change', "input[name='itemCheckbox']", function () {
        var allChecked = $("input[name='itemCheckbox']").length === $("input[name='itemCheckbox']:checked").length;
        $("#selectAll").prop('checked', allChecked);
        toggleViewSelectedButton();
    });

    // Handle View Selected button click
    $("#viewSelected").click(function () {
        var selectedIds = [];
        $("input[name='itemCheckbox']:checked").each(function () {
            selectedIds.push($(this).val());
        });

        if (selectedIds.length > 0) {
            // Show a SweetAlert confirmation dialog with a balance note
            Swal.fire({
                title: 'Confirm Selection',
                html: `
        <div style="text-align: left;">
            <p>You are about to complete your purchase with the selected items. Please review your selections and ensure you have sufficient funds to proceed.</p>
            <p style="color: #e74c3c; font-weight: bold; margin-top: 10px;">Important: If your balance is insufficient, you will be redirected to the recharge page to top up your account.</p>
        </div>
    `,
                icon: 'info',
                showCancelButton: true,
                confirmButtonText: 'Yes, proceed!',
                cancelButtonText: 'Cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Send selected IDs to the server if confirmed
                    $.ajax({
                        url: '/ProfileAccount/AccountProfileFinance/PurchaseShells',
                        type: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify({ ids: selectedIds }),
                        success: function (response) {
                            if (response.success && response.link != "") {
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
                                Swal.fire('Failed', 'No link returned from the server.', 'error');
                            }
                        },
                        error: function (xhr, status, error) {
                            Swal.fire('Error', 'Error sending selected items: ' + xhr.responseText, 'error');
                        }
                    });
                }
            });
        } else {
            // Show an alert if no items are selected
            Swal.fire('No items selected', 'Please select at least one item to proceed.', 'info');
        }
    });

    // Handle search input
    $("#searchBtn").on('click', function () {
        var searchValue = $('#kt_filter_search').val();
        StoreSearchInput(searchValue);
        $("#loadingIndicator").show();

        // Show the table and load data only when the search value is a 5-digit number
        $("#scriptList").show();
        GetPageData(1, 10, searchValue);
    });



});

function toggleViewSelectedButton() {
    var anyChecked = $("input[name='itemCheckbox']:checked").length > 0;
    $("#viewSelected").prop('disabled', !anyChecked);
}

// Store search input before making the request
function StoreSearchInput(searchValue) {
    // Prepare the data to be sent to the server
    var requestData = {
        searchInput: searchValue,
        searchModelType: 5 // Shell And Panel Link model type
    };

    // Store the search input via AJAX
    $.ajax({
        url: '/ProfileAccount/AccountProfileUsers/StoreSearchInput',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            console.log('Search input stored successfully.');
        },
        error: function (xhr, status, error) {
            console.error('Error storing search input: ' + xhr.responseText);
        }
    });
}
function GetPageData(pageNum, pageSize = 10, searchValue = '') {
    // Clear the table and load new data
    $("#scriptList").empty();
    // Show the loading indicator
    $("#loadingIndicator").show();
    // Make the AJAX request with the BIN and active status
    $.getJSON("/home/ShellAndPanelLinksList", {
        pageNumber: pageNum,
        pageSize: pageSize,
        GeneralString: searchValue, // Send  GeneralSearch
    }, function (response) {
        if (response.data.length === 0) {
            // Hide the loading indicator in case of an error
            $("#loadingIndicator").hide();
            $("#scriptList").append('<tr><td colspan="6" class="text-center">No Data Found!</td></tr>');
        } else {
            // Hide the loading indicator
            $("#loadingIndicator").hide();

            var rowData = "";
            for (var i = 0; i < response.data.length; i++) {
                rowData += '<tr>\
                            <td><input type="checkbox" name="itemCheckbox" value="' + response.data[i].id + '" /></td>\
                            <td>' + response.data[i].link + '**********</td>\
                             <td>' + response.data[i].price + '</td>\
                             <td><button data-shell-id="'+ response.data[i].id + '"  class="btn btn-success pay-card-btn" data-id="' + response.data[i].id + '">Buy</button></td>\
                            </tr > ';
            }
            $("#scriptList").append(rowData);
        }
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
    $('.pay-card-btn').on('click', function (e) {
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
                var creditId = $(this).data('shell-id');

                // Proceed with the AJAX request if confirmed
                $.ajax({
                    type: 'POST',
                    url: '/ProfileAccount/AccountProfileFinance/PurchaseShell/',
                    data: { id: creditId },
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


