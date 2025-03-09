"use strict";

// Class definition
var KTModalUserSearch = function () {
    // Private variables
    var element;
    var suggestionsElement;
    var resultsElement;
    var wrapperElement;
    var emptyElement;
    var searchObject;

    // Private functions
    var processs = function (search) {
        var timeout = setTimeout(function () {
            var number = KTUtil.getRandomInt(1, 3);

            // Hide recently viewed
            suggestionsElement.classList.add('d-none');

            if (number === 3) {
                // Hide results
                resultsElement.classList.add('d-none');
                // Show empty message 
                emptyElement.classList.remove('d-none');
                  
                    

            } else {
                
                // Show results
                resultsElement.classList.remove('d-none');
                let term = $('#term').val();
                $.get('/ControlPanel/AdminAdvisor/Search?term=' + term,
                    function (data) {
                        $(data).each(
                            function (index, item) {                              
                                   document.getElementById('resultSerach').innerHTML += '	<!--begin::User-->\
                                                       <div class="rounded d-flex flex-stack bg-active-lighten p-4" data-user-id="0">\
                                	<!--begin:: Details-->\
                                	<div class="d-flex align-items-center">\
                                		<!--begin::Checkbox-->\
                                		<label class="form-check form-check-custom form-check-solid me-5">\
                                			<input class="form-check-input" type="checkbox" name="users" data-kt-check="true" data-kt-check-target="[data-user-id='+ item.id + ']" value="' + item.id + '" />\
                                		</label>\
                                		<!--end::Checkbox-->\
                                		<!--begin::Avatar-->\
                                		<div class="symbol symbol-35px symbol-circle">\
                                       <img alt="user" src="data:image/png;base64,'+ item.image +'">\
                                		</div>\
                                		<!--end::Avatar-->\
                                		<!--begin::Details-->\
                                		<div class="ms-5">\
                                			<a href="/ControlPanel/AdminAdvisor/Edit/'+item.id+'" class="fs-5 fw-bold text-gray-900 text-hover-primary mb-2">'+ item.firstName + item.lastName +'</a>\
                                			<div class="fw-semibold text-muted">'+ item.email + '</div>\
                                		</div>\
                                		<!--end::Details-->\
                                	</div>\
                                	<!--end:: Details-->\
                                      </div>\
                                <!--end:: User-->\
                                <!--begin:: Separator-->\
                                <div class="border-bottom border-gray-300 border-bottom-dashed"></div>\
                                <!--end:: Separator--> ';
                            });
                    }
                );
                // Hide empty message 
                emptyElement.classList.add('d-none');
            }

            // Complete search
            search.complete();
        }, 1500);
    }

    var clear = function (search) {
        // Show recently viewed
        suggestionsElement.classList.remove('d-none');
        // Hide results
        resultsElement.classList.add('d-none');
        // Hide empty message 
        emptyElement.classList.add('d-none');
    }

    // Public methods
    return {
        init: function () {
            // Elements
            element = document.querySelector('#kt_modal_users_search_handler');

            if (!element) {
                return;
            }

            wrapperElement = element.querySelector('[data-kt-search-element="wrapper"]');
            suggestionsElement = element.querySelector('[data-kt-search-element="suggestions"]');
            resultsElement = element.querySelector('[data-kt-search-element="results"]');
            emptyElement = element.querySelector('[data-kt-search-element="empty"]');

            // Initialize search handler
            searchObject = new KTSearch(element);

            // Search handler
            searchObject.on('kt.search.process', processs);

            // Clear handler
            searchObject.on('kt.search.clear', clear);
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTModalUserSearch.init();
});