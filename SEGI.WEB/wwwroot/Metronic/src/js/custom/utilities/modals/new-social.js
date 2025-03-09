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
					'facebook': {
						validators: {
							notEmpty: {
								message: 'الرابط مطلوب'
							}
						}
					},
					'instagram': {
						validators: {
							notEmpty: {
								message: 'الرابط مطلوب'
							}
						}
					},
					'twitter': {
						validators: {
							notEmpty: {
								message: 'الرابط مطلوب'
							}
						}
					},
					'linkedIn': {
						validators: {
							notEmpty: {
								message: 'الرابط مطلوب'
							}
						}
					},
					'email': {
						validators: {
							regexp: {
								regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
								message: 'القيمة ليست عنوان بريد إلكتروني صالحًا',
							},
							notEmpty: {
								message: 'مطلوب عنوان البريد الإلكتروني'
							}
						}
					},
					'whatsapp': {
						validators: {
							regexp: {
								regexp: /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/,
								message: '05XXXXXXX or 05XXXXXXX  الرجاء ادخال رقم موبايل صحيح',
							},
							notEmpty: {
								message: 'رقم الهاتف مطلوب'
							}
						}
					},
					target_due_date: {
						validators: {
							notEmpty: {
								message: 'Target due date is required'
							}
						}
					},
					target_tags: {
						validators: {
							notEmpty: {
								message: 'Target tags are required'
							}
						}
					},
					'targets_notifications[]': {
						validators: {
							notEmpty: {
								message: 'Please select at least one communication method'
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
			let id = $("#name_id").val();
			let data = $("#kt_modal_new_social_form").serialize();
			let formData = new FormData();
			formData.append("FaceBook", $("#name_facebook").val());
			formData.append("Instagram", $("#name_instagram").val());
			formData.append("Twitter", $("#name_twitter").val());
			formData.append("LinkedIn", $("#name_linkedIn").val());
			formData.append("Email", $("#name_email").val());
			formData.append("Whatsapp", $("#name_whatsapp").val());
			formData.append("Id", $("#name_id").val());

			$.ajax({
				type: 'POST',
				url: '/ControlPanel/AdminSocialMediaLinks/Edit/'+ id+'',
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
							submitId();
						}, 2000);
					} else {
						// Show error message.
						Swal.fire({
							text: "المعذرة , يوجد خطأ بالبيانات المدخلة",
							icon: "error",
							buttonsStyling: false,
							confirmButtonText: "نعم لقد فهمت!",
							customClass: {
								confirmButton: "btn btn-light"
							}
						});
					}
				});
			}
		});

		cancelButton.addEventListener('click', function (e) {
			e.preventDefault();

			Swal.fire({
				text: "هل أنت متأكد من إلغاء العملية?",
				icon: "warning",
				showCancelButton: true,
				buttonsStyling: false,
				confirmButtonText: "نعم أنا متأكد!",
				cancelButtonText: "لا , رجوع",
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
						text: "العملية لم تتم ‘إلغائها!.",
						icon: "error",
						buttonsStyling: false,
						confirmButtonText: "نعم لقد فهمت!",
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
			modalEl = document.querySelector('#kt_modal_new_social');

			if (!modalEl) {
				return;
			}

			modal = new bootstrap.Modal(modalEl);

			form = document.querySelector('#kt_modal_new_social_form');
			submitButton = document.getElementById('kt_modal_new_social_submit');
			cancelButton = document.getElementById('kt_modal_new_social_cancel');

			initForm();
			handleForm();
		}
	};
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
	KTModalNewTarget.init();
});