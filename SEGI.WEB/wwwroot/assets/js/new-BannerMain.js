"use strict";

// Class definition
var KTModalNewTarget = function () {
    var submitButton;
    var cancelButton;
    var validator;
    var form;
    var modal;
    var modalEl;

    // Init form inputs
    var initForm = function () {
        // Due date. For more info, please visit the official plugin site: https://flatpickr.js.org/


        // Team assign. For more info, plase visit the official plugin site: https://select2.org/

    }

    // Handle form validation and submittion
    var handleForm = function () {
        // Stepper custom navigation

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );
        function submitId() {
            let id = $("#name_id").val();
            let data = $("#kt_modal_new_banneraMain_form").serialize();
            let image = $("#imageInput")[0].files[0];
            let content = $("#Content").val();
            let bannerPageType = $("#BannerPageType").val();
            let bannerContentType = $("#bannerContentType").val();

            let formData = new FormData();
            formData.append("Image", image);
            formData.append("Content", content);
            formData.append("BannerPageType", bannerPageType);
            formData.append("BannerContentType", bannerContentType);

            $.ajax({
                type: 'POST',
                url: '/ControlPanel/AdminBannerMain/Edit/' + id + '',
                //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                processData: false,
                contentType: false,
                data: formData,
                success: function (response) {
                    if (response.success) {

                        Swal.fire({
                            text: response.responseText,
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Add Success",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (result) {
                            if (result.isConfirmed) {
                                form.reset();
                                modal.hide();
                                window.location.reload();
                            }
                        });
                    } else {
                        // DoSomethingElse()
                        Swal.fire({
                            text: response.responseText,
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "try Again",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    };
                },
                error: function () {
                    Swal.fire({
                        text: "Invalid Data ,Try Again!",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Try Again!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    });
                }
            })
        }
        // Action buttons
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable button to avoid multiple click 
                        submitButton.disabled = true;

                        setTimeout(function () {
                            submitButton.removeAttribute('data-kt-indicator');

                            // Enable button
                            submitButton.disabled = false;
                            submitId();
                        }, 2000);
                    } else {
                        // Show error message.
                        Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                });
            }
        });

        cancelButton.addEventListener('click', function (e) {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal			
                    window.location = "Edit";

                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });
    }

    return {
        // Public functions
        init: function () {
            // Elements
            modalEl = document.querySelector('#kt_modal_new_banneraMain');

            if (!modalEl) {
                return;
            }

            modal = new bootstrap.Modal(modalEl);

            form = document.querySelector('#kt_modal_new_banneraMain_form');
            submitButton = document.getElementById('kt_modal_new_banneraMain_submit');
            cancelButton = document.getElementById('kt_modal_new_banneraMain_cancel');

            initForm();
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTModalNewTarget.init();
});