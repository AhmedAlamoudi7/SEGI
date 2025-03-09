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
		var dueDate = $(form.querySelector('[name="due_date"]'));
		dueDate.flatpickr({
			enableTime: true,
			dateFormat: "d, M Y, H:i",
		});

		// Team assign. For more info, plase visit the official plugin site: https://select2.org/
		$(form.querySelector('[name="team_assign"]')).on('change', function () {
			// Revalidate the field when an option is chosen
			validator.revalidateField('team_assign');
		});
	}

	// Handle form validation and submittion
	var handleForm = function () {
		// Stepper custom navigation

		// Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
		validator = FormValidation.formValidation(
			form,
			{
				fields: {
					'fullName': {
						validators: {
							notEmpty: {
								message: 'Full Name Is Required'
							}
						}
					},
					'email': {
						validators: {
							notEmpty: {
								message: 'Email Is Required'
							}
						}
					},
	  
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

			let formData = new FormData();
			var userId = $("#userId").val();
			formData.append("id", $("#userId").val());
 			formData.append("fullname", $("#fullName").val());
 			formData.append("email", $("#email").val());
			formData.append("userType", $("#userType").val());
			formData.append("image", $("#image").val());

			$.ajax({
				type: 'POST',
				url: '/ProfileAccount/AccountProfileUsers/EditUser?id= ' + userId,
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
							confirmButtonText: "تمت الاضافة",
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
							confirmButtonText: "اعادة المحاولة",
							customClass: {
								confirmButton: "btn btn-primary"
							}
						});
					};
				},
				error: function () {
					Swal.fire({
						text: "عذرا يوجد خطأ بالبيانات , الرجاء حاول مرة أخرى",
						icon: "error",
						buttonsStyling: false,
						confirmButtonText: "اعادة المحاولة",
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

							// Show success message. For more info check the plugin's official documentation: https://sweetalert2.github.io/
							//Swal.fire({
							//	text: "Form has been successfully submitted!",
							//	icon: "success",
							//	buttonsStyling: false,
							//	confirmButtonText: "Ok, got it!",
							//	customClass: {
							//		confirmButton: "btn btn-primary"
							//	}
							//}).then(function (result) {
							//	if (result.isConfirmed) {
							//		modal.hide();
							//	}
							//});
							submitId();
							//$.ajax({
							//	type: 'POST',
							//	url: '/ControlPanel/AdminAdvisor/Create',
							//	data: { interfaceService: form },
							//	success: function (result) {
							//		location.reload();
							//	},
							//	error: function () {
							//		alert('Failed to receive the Data');
							//	}
							//});
							/*form.submit();*/ // Submit form
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
			modalEl = document.querySelector('#kt_account_settings_profile_details');

			if (!modalEl) {
				return;
			}

			modal = new bootstrap.Modal(modalEl);

			form = document.querySelector('#kt_account_profile_details_form');
			submitButton = document.getElementById('kt_account_profile_details_submit');
			cancelButton = document.getElementById('kt_account_profile_details_cancel');

			initForm();
			handleForm();
		}
	};
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
	KTModalNewTarget.init();
});