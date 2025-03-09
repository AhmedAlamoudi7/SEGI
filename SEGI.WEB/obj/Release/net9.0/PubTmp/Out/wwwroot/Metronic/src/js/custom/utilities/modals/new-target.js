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
		var skills = new Tagify(form.querySelector('[name="createAdviserDto.skills"]'), {
			whitelist: ["Important", "Urgent", "High", "Medium", "Low"],
			maxTags: 5,
			dropdown: {
				maxItems: 15,           // <- mixumum allowed rendered suggestions
				enabled: 0,             // <- show suggestions on focus
				closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
			}
		});
		skills.on("change", function(){
			// Revalidate the field when an option is chosen
			validator.revalidateField('createAdviserDto.skills');
		});

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
					'createAdviserDto.firstName': {
						validators: {
							notEmpty: {
								message: 'الاسم الأول مطلوب'
							}
						}
					},
					'createAdviserDto.lastName': {
						validators: {
							notEmpty: {
								message: 'الاسم الأخير مطلوب'
							}
						}
					},
					'createAdviserDto.cv': {
						validators: {
							notEmpty: {
								message: 'السيرة الذاتية مطلوبة'
							}
						}
					},
					'createAdviserDto.email': {
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
					'createAdviserDto.phone': {
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
					'createAdviserDto.specialization': {
						validators: {
							notEmpty: {
								message: 'التخصص مطلوب'
							}
						}
					},'createAdviserDto.degree': {
						validators: {
							notEmpty: {
								message: 'الدرجة العلمية مطلوب'
							}
						}
					},
					'createAdviserDto.degree': {
						validators: {
							notEmpty: {
								message: 'الدرجة العلمية مطلوبة'
							}
						}
					},
					'createAdviserDto.createAdviserDto.skills': {
						validators: {
							notEmpty: {
								message: 'المهارات مطلوبة'
							}
						}
					},
					'createAdviserDto.password': {
						validators: {
							notEmpty: {
								message: 'كلمة المرور مطلوبة'
							},
							callback: {
								message: 'الرجاء إدخال كلمة مرور صالحة',
								callback: function (input) {
									if (input.value.length > 0) {
										return validatePassword();
									}
								}
							}
						}
					},

					'createAdviserDto.confirmpassword': {
						validators: {
							notEmpty: {
								message: 'مطلوب تأكيد كلمة المرور'
							},
							identical: {
								compare: function () {
									return form.querySelector('[name="createAdviserDto.password"]').value;
								},
								message: 'كلمة المرور وتأكيدها ليسا متطابقين'
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
			//var dataO = {
			//	firstName: $('#id="firstName').val()
			//};
			//var json = JSON2.stringify(dataO); 
			let data = $("#kt_modal_new_target_form").serialize();
			let special = $('#specializationId option:selected').val();
			let degree = $('#degreeId option:selected').val();
			let skills = $("#skills").val();
			String(skills);
			let formData = new FormData();
			formData.append("CV", $("#cv")[0].files[0]);
			formData.append("FirstName", $("#firstName").val());
			formData.append("LastName", $("#lastName").val());
			formData.append("Email", $("#email").val());
			formData.append("Phone", $("#phone").val());
			formData.append("DegreeId", degree);
			formData.append("SpecializationId", special);
			formData.append("Skills", skills);
			formData.append("Password", $("#password").val());
			formData.append("ConfirmPassword", $("#confirmpassword").val());

			$.ajax({
				type: 'POST',
				url: '/ControlPanel/AdminAdvisor/Create',
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
			modalEl = document.querySelector('#kt_modal_new_target');

			if (!modalEl) {
				return;
			}

			modal = new bootstrap.Modal(modalEl);

			form = document.querySelector('#kt_modal_new_target_form');
			submitButton = document.getElementById('kt_modal_new_target_submit');
			cancelButton = document.getElementById('kt_modal_new_target_cancel');

			initForm();
			handleForm();
		}
	};
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
	KTModalNewTarget.init();
});