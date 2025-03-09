$(document).ready(function () {
    var fixedPricePer10 = 0; // Initialize to store the fixed price

    // Load all items on page load
    GetPageData();

    // Retrieve the fixed price from the server on page load
    $.ajax({
        url: '/ProfileAccount/AccountProfileFinance/GetFixedPrice',
        type: 'GET',
        success: function (response) {
            if (response.fixedPrice && !isNaN(response.fixedPrice)) {
                fixedPricePer10 = response.fixedPrice;
                console.log("Fixed price per 100 records is: $" + fixedPricePer10);
                updateTotalPrice(); // Update the initial total price after retrieval
            } else {
                console.error('Invalid fixed price received.');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error retrieving fixed price: ' + xhr.responseText);
            fixedPricePer10 = 1; // Fallback to $1 if the request fails
        }
    });



    // Handle search input
    $("#searchBtn").on('click', function () {
        var searchValue = $('#kt_filter_search').val();
        StoreSearchInput(searchValue);
        GetPageData(searchValue, function () {
        });
    });

    function showInitialLinks() {
        let initialLinksHtml = '';
        const fakeAccounts = [
            { id: 1, username: 'john_doe123', password: 'password123', link: 'https://linkedin.com/in/john_doe123' },
            { id: 2, username: 'jane_fakesmith', password: 'password456', link: 'https://twitter.com/jane_fakesmith' },
            { id: 3, username: 'fakeguru', password: 'fakeguru789', link: 'https://instagram.com/fakeguru' },
            { id: 4, username: 'notreal_person', password: 'notreal000', link: 'https://facebook.com/notreal_person' },
            { id: 5, username: 'ghost_account', password: 'ghostpass', link: 'https://pinterest.com/ghost_account' },
            { id: 6, username: 'dummy_999', password: 'dummy999', link: 'https://reddit.com/user/dummy_999' },
            { id: 7, username: 'fake_influencer', password: 'influencer123', link: 'https://youtube.com/fake_influencer' },
            { id: 8, username: 'test_account1', password: 'testpass1', link: 'https://github.com/test_account1' },
            { id: 9, username: 'anon_user_x', password: 'anonpass', link: 'https://tiktok.com/@anon_user_x' },
            { id: 10, username: 'faker_baker', password: 'baker456', link: 'https://medium.com/@faker_baker' },
        ];

        fakeAccounts.forEach(account => {
            initialLinksHtml += `<tr>
                         <td><a href="${account.link}" target="_blank" class="modern-link">${account.link}</a></td>
                         <td>${account.username}</td>
                         <td>${account.password}</td>
                     </tr>`;
        });

        initialLinksHtml += `<tr>
                            <td colspan="3" style="text-align: center; padding-top: 20px;">
                                <a id="showMoreLink" href="#" class="show-more-link" style="display: inline-block; text-decoration: none; font-weight: bold;">Show More 100000+ Account <span style="font-size: 1.2em;">&#x25BC;</span></a>
                            </td>
                         </tr>`;

        $("#initialLinks").html(`<table>${initialLinksHtml}</table>`);
        $("#initialLinks").show();

        // Add event listener for "Show More" link to open modal
        $(document).on('click', '#showMoreLink', function (event) {
            event.preventDefault();
            console.log('Please use the search feature to view more details about the accounts you need.');
        });
    }

    // Add modern link styling
    $('<style>.modern-link { color: #15B5DB; text-decoration: none; font-weight: bold; } .modern-link:hover { text-decoration: underline; } .show-more-link { color: #15B5DB; text-decoration: none; } .show-more-link:hover { text-decoration: underline; }</style>').appendTo('head');

    // Store search input before making the request
    function StoreSearchInput(searchValue) {
        var requestData = {
            searchInput: searchValue,
            searchModelType: 6 // Data Collection Account Link model type
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

    function getTotalFoundRecords() {
        return $("#totalRecords").val(); // Replace with the actual logic
    }

    function updateTotalPrice() {
        var recordCount = $('#recordCount').val();
        if (fixedPricePer10 > 0) {
            var totalPrice = Math.ceil(recordCount / 100) * fixedPricePer10; // Use the fixed price for every 10 records
            $('#totalPrice').text(totalPrice);
            console.log("Total price updated: $" + totalPrice);
        } else {
            console.warn('Fixed price is not properly set.');
        }
    }

    var totalFoundRecords = getTotalFoundRecords();
    if (totalFoundRecords < 100 && totalFoundRecords > 0) {
        $('#recordCount').val(totalFoundRecords).prop('disabled', true); // Set recordCount and disable it
        updateTotalPrice(); // Update the price based on the new record count
    }

    $('#recordCount').on('input', function () {
        if (!$(this).prop('disabled')) { // Only update if not disabled
            updateTotalPrice();
        }
    });

    $('#payButton').on('click', function (e) {
        e.preventDefault();

        var searchWord = $('#kt_filter_search').val();
        var recordCount = $('#recordCount').val();
        var totalPrice = Math.ceil(recordCount / 100) * fixedPricePer10; // Use fixed price for every 10 records

        if (recordCount < 100) {
            if (totalFoundRecords > 100) {
                Swal.fire('Info', 'You must purchase at least 10 records.', 'info');
                return;
            } else if (totalFoundRecords === 0) {
                Swal.fire('Info', 'No records found for your search.', 'info');
                return;
            }
        }

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
            confirmButtonText: 'Yes, proceed!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // Proceed with AJAX request
                $.ajax({
                    url: $(this).attr('href') + "?generalSearch=" + searchWord + "&recordCount=" + recordCount + "&totalPrice=" + totalPrice,
                    type: 'POST',
                    contentType: 'application/json',
                    success: function (response) {
                        if (!response.success) {
                            Swal.fire('Error', 'There was an issue with your purchase. Please try again later.', 'error');
                        } else {
                            // Check if balance is insufficient and redirect to charge page
                            if (response.link === "/ProfileAccount/AccountProfileFinance/ChargeBalance") {
                                Swal.fire({
                                    title: 'Insufficient Balance',
                                    text: 'Your balance is insufficient. You will be redirected to charge your balance.',
                                    icon: 'warning',
                                    confirmButtonText: 'Charge Balance'
                                }).then(() => {
                                    window.location.href = response.link;
                                });
                            } else {
                                // Show success message with balance reduction confirmation if balance is sufficient
                                Swal.fire({
                                    title: 'Purchase Confirmation',
                                    text: 'The purchase reduce your account balance',
                                    icon: 'info',
                                    showCancelButton: false,
                                    confirmButtonText: 'Okay!'
                                }).then((balanceConfirm) => {
                                    if (balanceConfirm.isConfirmed) {
                                        window.location.href = response.link;
                                    }
                                });
                            }
                        }
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while processing your request. Please try again later.', 'error');
                    }
                });
            }
        });
    });
    function GetPageData(searchValue = '') {
        $("#scriptList").empty();
        $("#paymentSection").hide();

        // Check if this is a search operation
        const isSearch = searchValue !== '';

        if (isSearch) {
            $("#loadingIndicator").show();
            $("#initialLinks").hide(); // Hide initial links during search
            $.getJSON("/home/DataCollectionAccountList", {
                GeneralString: searchValue,
            }, function (response) {
                if (response > 0) {
                    $("#loadingIndicator").hide();

                    $("#scriptList").html('<div class="alert alert-info text-center">Total Records Found: ' + response + '</div>');
                    $("#totalRecords").text(response);
                    $("#paymentSection").show();

                    if (response < 10) {
                        $('#recordCount').val(response).prop('disabled', true); // Set and disable recordCount
                        updateTotalPrice(); // Update the price based on the new record count
                    }
                } else if (response <= 0) {
                    if (isSearch) {
                        $("#loadingIndicator").hide();
                        $("#scriptList").html('<div class="alert alert-danger text-center">No Data Found!</div>');
                    }
                } else {
                    $("#totalRecords").text(0);
                    $("#paymentSection").hide();
                }
            });

        } else {
            $("#initialLinks").show(); // Show initial links when not searching
        }


    }

    // Call the function to show initial links on page load
    showInitialLinks();
});