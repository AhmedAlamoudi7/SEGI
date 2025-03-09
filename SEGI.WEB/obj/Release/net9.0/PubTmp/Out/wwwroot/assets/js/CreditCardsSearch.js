$(document).ready(function () {
    // Initially hide the table and disable the "View Selected" button
    $("#viewSelected").prop('disabled', true);

    // Load all data when the page is loaded or when the search input is empty
    GetPageData(1, 100); // Fetch all data initially if the input is empty.
    // Handle "Select All" checkbox
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
    // Handle search button click
    $("#searchBtn").on('click', function () {
        var searchValue = $('#kt_filter_search').val().trim();
        var selectedStatus = $('#statusFilter').val(); // Get selected status ('live', 'declined', or '' for all)
        $("#loadingIndicator").show();

        // If the input field is empty, get all data
        if (searchValue === "") {
            // Fetch all data if the search field is empty
            if (selectedStatus === "") {
                // If no status is selected, fetch all data
                GetPageData(1, 100); // Fetch all data
            } else {
                // If a status (live or declined) is selected, fetch data based on the status
                GetPageData(1, 100, "", selectedStatus); // Fetch data filtered by the selected status
            }
        } else {
            // Validate BIN or Card Number length (6 or more digits)
            if (searchValue.length < 6) {
                Swal.fire('Invalid Input', 'Please enter at least 6 digits for the BIN or Card Number.', 'warning');
                $('#kt_filter_search').addClass('invalid-bin'); // Add invalid styling to input
                $('#searchBtn').addClass('disabled');  // Disable the search button
                return;  // Stop further execution
            } else {
                $('#kt_filter_search').removeClass('invalid-bin'); // Remove invalid styling
                $('#searchBtn').removeClass('disabled'); // Enable the search button
                $("#viewSelected").prop('disabled', true);

            }

            // Store search input for server
            StoreSearchInput(searchValue);

            // Proceed if the search value is valid
            $("#scriptList").show();
            GetPageData(1, 10, searchValue, selectedStatus);
        }
    });
    // Handle "View Selected" button click
    $("#viewSelected").click(function () {
        var selectedIds = [];
        $("input[name='itemCheckbox']:checked").each(function () {
            selectedIds.push($(this).val());
        });

        if (selectedIds.length > 0) {
            // Show a SweetAlert confirmation dialog with a balance note
            Swal.fire({
                title: 'Confirm Checkout',
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
                        url: '/ProfileAccount/AccountProfileFinance/PurchaseCreditCards',
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
                                    icon: 'info',
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

    // Enable/Disable Search Button based on input length
    $('#kt_filter_search').on('input', function () {
        var searchValue = $(this).val().trim();
        if (searchValue.length < 6 && searchValue.length > 0) {
            $('#kt_filter_search').addClass('invalid-bin');
            $('#searchBtn').addClass('disabled');
        } else {
            $('#kt_filter_search').removeClass('invalid-bin');
            $('#searchBtn').removeClass('disabled');
        }
    });
    // Enable or disable "View Selected" button based on checkbox selection
    function toggleViewSelectedButton() {
        var anyChecked = $("input[name='itemCheckbox']:checked").length > 0;
        $("#viewSelected").prop('disabled', !anyChecked);
    }
});

// Store search input before making the request
function StoreSearchInput(searchValue) {
    var requestData = {
        searchInput: searchValue,
        searchModelType: 2 // CreditCard model type
    };

    $.ajax({
        url: '/ProfileAccount/AccountProfileUsers/StoreSearchInput',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function () {
            console.log('Search input stored successfully.');
        },
        error: function (xhr) {
            console.error('Error storing search input: ' + xhr.responseText);
        }
    });
}

// Fetch page data and update the table
function GetPageData(pageNum, pageSize = 10, searchValue = '', selectedStatus = '') {
    let bin = searchValue.trim(); // We will search using either the BIN or full card number
    let activeStatus = null;  // Default: no filter
    // Show the loading indicator
    $("#loadingIndicator").show();

    // If the status is selected, filter based on the status
    if (selectedStatus === 'live') {
        activeStatus = true;
    } else if (selectedStatus === 'declined') {
        activeStatus = false;
    }

    // If no search value is provided, we fetch all data
    if (bin === "") {
        bin = null; // No filter for BIN, meaning get all data
    }

    // Make the AJAX request with the parsed BIN or Card number and status
    $.getJSON("/home/CreditCardsList", {
        pageNumber: pageNum,
        pageSize: pageSize,
        GeneralSearch: bin, // Send the BIN or card number as GeneralSearch
        Active: activeStatus // Send active status as Active
    }, function (response) {
        if (response.data.length > 0) {
            $("#loadingIndicator").hide();
            // Clear previous data
            $("#scriptList").empty();
            // Hide the loading indicator
            $("#loadingIndicator").hide();
            // Generate new rows
            var rowData = "";
            for (var i = 0; i < response.data.length; i++) {
                rowData += `<tr>
                            <td><input type="checkbox" name="itemCheckbox" value="${response.data[i].id}" /></td>
                            <td>${response.data[i].binInfo}**********</td>
                            <td> ************</td>
                            <td>**/**</td>
                            <td>***</td>
                            <td>${response.data[i].type}</td>
                            <td>${response.data[i].brand}</td>
                            <td>${response.data[i].bankName}</td>
                            <td>${response.data[i].country}</td>
                            <td>${response.data[i].active ? 'live' : 'Declined'}</td>
                            <td>${response.data[i].price}</td>
                            <td><button data-credit-id="${response.data[i].id}" class="btn btn-success pay-card-btn">Buy</button></td>
                        </tr>`;
            }
            $("#scriptList").append(rowData);
            PaggingTemplate(response.totalPages, response.currentPage);
        } else {
            // Hide the loading indicator in case of an error
            $("#loadingIndicator").hide();
            $("#scriptList").empty().append('<tr><td colspan="12">No data found.</td></tr>');
        }
    }).fail(function (xhr) {
        console.error('Error loading data: ' + xhr.responseText);
        alert('Error loading data: ' + xhr.responseText);
        // Hide the loading indicator in case of an error
        $("#loadingIndicator").hide();
    });
}

// Pagination template generation
function PaggingTemplate(totalPage, currentPage) {
    var template = '';
    var pageNumbers = [];

    for (var i = currentPage; i <= totalPage && pageNumbers.length < 5; i++) {
        pageNumbers.push(i);
    }

    var prevPage = (currentPage > 1) ? currentPage - 1 : 1;
    var nextPage = (currentPage < totalPage) ? currentPage + 1 : totalPage;

    template += `<ul class="pagination flex-wrap mt-20">
                 <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
                 <a href="#" class="page-link" onclick="GetPageData(${prevPage})"><i class="fas fa-angle-double-left"></i></a></li>`;

    pageNumbers.forEach(page => {
        template += `<li class="page-item ${page === currentPage ? 'active' : ''}">
                     <a href="#" class="page-link" onclick="GetPageData(${page})">${page}</a></li>`;
    });

    template += `<li class="page-item ${currentPage === totalPage ? 'disabled' : ''}">
                 <a href="#" class="page-link" onclick="GetPageData(${nextPage})"><i class="fas fa-angle-double-right"></i></a></li>
                 </ul>`;

    $("#paged").empty().append(template);
    // Add click event for the Buy Now button
    $('.pay-card-btn').on('click', function (e) {
        e.preventDefault();

        // Show a SweetAlert confirmation dialog with a balance note
        Swal.fire({
            title: 'Confirm Purchase',
            html: `
            <p>Do you want to proceed with this purchase?</p>
            <p style="color: #e74c3c; font-weight: bold; margin-top: 10px;">Note: Ensure you have sufficient balance, or you’ll be redirected to the recharge page.</p>
        `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, buy now!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // Get the script ID from the data attribute
                var creditId = $(this).data('credit-id');

                // Proceed with the AJAX request if confirmed
                $.ajax({
                    type: 'POST',
                    url: '/ProfileAccount/AccountProfileFinance/PurchaseCreditCard/',
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
