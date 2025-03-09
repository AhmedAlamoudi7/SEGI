"use strict";

// Class definition
var KTAuthI18nDemo = function() {
    // Elements
    var menu;
	
	var menuObj;

	var translationStrings = {
		// General
		"general-progress" : {
			"English" : "الرجاء الإنتظار ...",
			"Arabic" :"Please wait...",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},
		"general-desc" : {
			"English" : "احصل على وصول غير محدود واكسب المال",
			"Spanish" : "Obtenga acceso ilimitado y gane dinero",
			"German" : "Erhalten Sie unbegrenzten Zugriff und verdienen Sie Geld",
			"Japanese" : "無制限のアクセスを取得してお金を稼ぐ",
			"French" : "Obtenez un accès illimité et gagnez de l'argent",
		},

		"general-or" : {
			"English" : "أو",
			"Spanish" : "O",
			"German" : "Oder",
			"Japanese" : "または",
			"French" : "Ou",
		},

		// Sign in	
		"sign-in-head-desc" : {
			"English" : "لست عضوا حتى الآن ؟",
			"Spanish" : "¿No eres miembro todavía?",
			"German" : "Noch kein Mitglied?",
			"Japanese" : "まだメンバーではありませんか？",
			"French" : "Pas encore membre?",
		},	
		 
		"sign-in-head-link" : {
			"English" : "انشاء حساب",
			"Spanish" : "Inscribirse",
			"German" : "Anmeldung",
			"Japanese" : "サインアップ",
			"French" : "S'S'inscrire",
		},	 

		"sign-in-title" : {
			"English" : "تسجيل دخول",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		"sign-in-input-email" : {
			"English" : "البريد الالكتروني",
			"Spanish" : "Correo electrónico",
			"German" : "Email",
			"Japanese" : "Eメール",
			"French" : "E-mail",
		},

		"sign-in-input-password" : {
			"English" : "كلمة المرور",
			"Spanish" : "Clave",
			"German" : "Passwort",
			"Japanese" : "パスワード",
			"French" : "Mot de passe",
		},

		"sign-in-forgot-password" : {
			"English" : "هل نسيت كلمة المرور ؟",
			"Spanish" : "Has olvidado tu contraseña ?",
			"German" : "Passwort vergessen ?",
			"Japanese" : "パスワードをお忘れですか ？",
			"French" : "Mot de passe oublié ?",
		},

		"sign-in-submit" : {
			"English" : "تسجيل دخول",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		// Sing up
		"sign-up-head-desc" : {
			"English" : "عضوا فعلا ؟",
			"Spanish" : "Ya eres usuario ?",
			"German" : "Schon ein Mitglied ?",
			"Japanese" : "すでにメンバーですか？",
			"French" : "Déjà membre ?",
		},	

		"sign-up-head-link" : {
			"English" : "تسجيل دخول",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},
		
		"sign-up-title" : {
			"English" : "انشاء حساب",
			"Spanish" : "Inscribirse",
			"German" : "Anmeldung",
			"Japanese" : "サインアップ",
			"French" : "S'S'inscrire",
		},	

		"sign-up-input-first-name" : {
			"English" : "الاسم الاول",
			"Spanish" : "Primer nombre",
			"German" : "Vorname",
			"Japanese" : "ファーストネーム",
			"French" : "Prénom",
		},

		"sign-up-input-last-name" : {
			"English" : "الاسم الاخير",
			"Spanish" : "Apellido",
			"German" : "Nachname",
			"Japanese" : "苗字",
			"French" : "Nom de famille",
		},

		"sign-up-input-email" : {
			"English" : "البريد الالكتروني",
			"Spanish" : "Correo electrónico",
			"German" : "Email",
			"Japanese" : "Eメール",
			"French" : "E-mail",
		},

		"sign-up-input-password" : {
			"English" : "كلمة المرور",
			"Spanish" : "Clave",
			"German" : "Passwort",
			"Japanese" : "パスワード",
			"French" : "Mot de passe",
		},

		"sign-up-input-confirm-password" : {
			"English" : "كلمة المرور",
			"Spanish" : "Clave",
			"German" : "Passwort",
			"Japanese" : "パスワード",
			"French" : "Mot de passe",
		},

		"sign-up-submit" : {
			"English" : "تسجيل",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		"sign-up-hint" : {
			"English" : "استخدم 8 أحرف أو أكثر مع مزيج من الأحرف والأرقام والرموز.",
			"Spanish" : "Utilice 8 o más caracteres con una combinación de letras, números y símbolos.",
			"German" : "Verwenden Sie 8 oder mehr Zeichen mit einer Mischung aus Buchstaben, Zahlen und Symbolen.",
			"Japanese" : "文字、数字、記号を組み合わせた8文字以上を使用してください。",
			"French" : "Utilisez 8 caractères ou plus avec un mélange de lettres, de chiffres et de symboles.",
		},

		// New password
		"new-password-head-desc" : {
			"English" : "عضوا فعلا ؟",
			"Spanish" : "Ya eres usuario ?",
			"German" : "Schon ein Mitglied ?",
			"Japanese" : "すでにメンバーですか？",
			"French" : "Déjà membre ?",
		},

		"new-password-head-link" : {
			"English" : "تسجيل دخول",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		"new-password-title" : {
			"English" : "قم بإعداد كلمة مرور جديدة",
			"Spanish" : "Configurar nueva contraseña",
			"German" : "Neues Passwort einrichten",
			"Japanese" : "新しいパスワードを設定する",
			"French" : "Configurer un nouveau mot de passe",
		},

		"new-password-desc" : {
			"English" : "هل قمت بالفعل بإعادة تعيين كلمة المرور؟",
			"Spanish" : "¿Ya has restablecido la contraseña?",
			"German" : "Hast du das Passwort schon zurückgesetzt?",
			"Japanese" : "すでにパスワードをリセットしましたか？",
			"French" : "Avez-vous déjà réinitialisé le mot de passe ?",
		},

		"new-password-input-password" : {
			"English" : "كلمة المرور",
			"Spanish" : "Clave",
			"German" : "Passwort",
			"Japanese" : "パスワード",
			"French" : "Mot de passe",
		},

		"new-password-hint" : {
			"English" : "استخدم 8 أحرف أو أكثر مع مزيج من الأحرف والأرقام والرموز.",
			"Spanish" : "Utilice 8 o más caracteres con una combinación de letras, números y símbolos.",
			"German" : "Verwenden Sie 8 oder mehr Zeichen mit einer Mischung aus Buchstaben, Zahlen und Symbolen.",
			"Japanese" : "文字、数字、記号を組み合わせた8文字以上を使用してください。",
			"French" : "Utilisez 8 caractères ou plus avec un mélange de lettres, de chiffres et de symboles.",
		},

		"new-password-confirm-password" : {
			"English" : "تأكيد كلمة المرور",
			"Spanish" : "Confirmar contraseña",
			"German" : "Passwort bestätigen",
			"Japanese" : "パスワードを認証する",
			"French" : "Confirmez le mot de passe",
		},

		"new-password-submit" : {
			"English" : "تسجيل",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		// Password reset
		"password-reset-head-desc" : {
			"English" : "عضوا فعلا ؟",
			"Spanish" : "Ya eres usuario ?",
			"German" : "Schon ein Mitglied ?",
			"Japanese" : "すでにメンバーですか？",
			"French" : "Déjà membre ?",
		},

		"password-reset-head-link" : {
			"English" : "تسجيل الدخول",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		"password-reset-title" : {
			"English" : "هل نسيت كلمة المرور ؟",
			"Spanish" : "Has olvidado tu contraseña ?",
			"German" : "Passwort vergessen ?",
			"Japanese" : "パスワードをお忘れですか ？",
			"French" : "Mot de passe oublié ?",
		},

		"password-reset-desc" : {
			"English" : "أدخل بريدك الإلكتروني لإعادة تعيين كلمة المرور الخاصة بك.",
			"Spanish" : "Ingrese su correo electrónico para restablecer su contraseña.",
			"German" : "Geben Sie Ihre E-Mail-Adresse ein, um Ihr Passwort zurückzusetzen.",
			"Japanese" : "メールアドレスを入力してパスワードをリセットしてください。",
			"French" : "Entrez votre e-mail pour réinitialiser votre mot de passe.",
		},

		"password-reset-input-email" : {
			"English" : "البريد الإلكتروني",
			"Spanish" : "Correo electrónico",
			"German" : "Email",
			"Japanese" : "Eメール",
			"French" : "E-mail",
		},

		"password-reset-submit" : {
			"English" : "تسجيل",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		"password-reset-cancel" : {
			"English" : "الغاء",
			"Spanish" : "Cancelar",
			"German" : "Absagen",
			"Japanese" : "キャンセル",
			"French" : "Annuler",
		},

		// Two steps	
		"two-step-head-desc" : {
			"English" : "ألم تحصل على الرمز؟",
			"Spanish" : "¿No recibiste el código?",
			"German" : "Code nicht erhalten?",
			"Japanese" : "コードを取得できませんでしたか？",
			"French" : "Vous n'avez pas reçu le code ?",
		},	

		"two-step-head-resend" : {
			"English" : "إعادة إرسال",
			"Spanish" : "Reenviar",
			"German" : "Erneut senden",
			"Japanese" : "再送",
			"French" : "Renvoyer",
		},

		"two-step-head-or" : {
			"English" : "أو",
			"Spanish" : "O",
			"German" : "Oder",
			"Japanese" : "または",
			"French" : "Ou",
		},

		"two-step-head-call-us" : {
			"English" : "اتصل بنا",
			"Spanish" : "Llámenos",
			"German" : "Rufen Sie uns an",
			"Japanese" : "お電話ください",
			"French" : "Appelez-nous",
		},

		"two-step-submit" : {
			"English" : "تسجيل",
			"Spanish" : "Iniciar Sesión",
			"German" : "Registrarse",
			"Japanese" : "ログイン",
			"French" : "S'identifier",
		},

		"two-step-title" : {
			"English" : "التحقق بخطوتين",
			"Spanish" : "Verificación de dos pasos",
			"German" : "Verifizierung in zwei Schritten",
			"Japanese" : "2段階認証",
			"French" : "Vérification en deux étapes",
		},

		"two-step-deck" : {
			"English" : "أدخل رمز التحقق الذي أرسلناه إلى",
			"Spanish" : "Ingresa el código de verificación que enviamos a",
			"German" : "Geben Sie den von uns gesendeten Bestätigungscode ein",
			"Japanese" : "送信した確認コードを入力してください",
			"French" : "Entrez le code de vérification que nous avons envoyé à",
		},

		"two-step-label" : {
			"English" : "اكتب رمز الحماية المكون من 6 أرقام",
			"Spanish" : "Escriba su código de seguridad de 6 dígitos",
			"German" : "Geben Sie Ihren 6-stelligen Sicherheitscode ein",
			"Japanese" : "6桁のセキュリティコードを入力します",
			"French" : "Tapez votre code de sécurité à 6 chiffres",
		}
	}

    // Handle form
    var translate = function(lang) {
        for (var label in translationStrings) {
			if (translationStrings.hasOwnProperty(label)) {
				if (translationStrings[label][lang]) {
					let labelElement = document.querySelector('[data-kt-translate=' + label + ']');

					if (labelElement !== null) {
						if (labelElement.tagName === "INPUT") {
							labelElement.setAttribute("placeholder", translationStrings[label][lang]);
						} else {
							labelElement.innerHTML = translationStrings[label][lang];
						}						
					}
				}
			}
		}
    }

	var setLanguage = function(lang) {
		const selectedLang = menu.querySelector('[data-kt-lang="' + lang + '"]');

		if (selectedLang !== null) {
			const currentLangName = document.querySelector('[data-kt-element="current-lang-name"]'); 
			const currentLangFlag = document.querySelector('[data-kt-element="current-lang-flag"]'); 

			const selectedLangName = selectedLang.querySelector('[data-kt-element="lang-name"]');
			const selectedLangFlag = selectedLang.querySelector('[data-kt-element="lang-flag"]');

			currentLangName.innerText = selectedLangName.innerText;
			currentLangFlag.setAttribute("src", selectedLangFlag.getAttribute("src"));

			localStorage.setItem("kt_auth_lang", lang);
		}
	}

	var init = function() {
		if ( localStorage.getItem("kt_auth_lang") !== null ) {
			let lang = localStorage.getItem("kt_auth_lang");
			
			setLanguage(lang);
			translate(lang);
		}

		menuObj.on("kt.menu.link.click", function(element) {
			let lang = element.getAttribute("data-kt-lang");

			setLanguage(lang);
			translate(lang);
		});
	}

    // Public functions
    return {
        // Initialization
        init: function() {
            menu = document.querySelector('#kt_auth_lang_menu');

			if (menu === null) {
				return;
			} 

			menuObj = KTMenu.getInstance(menu);
            
            init();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function() {
    KTAuthI18nDemo.init();
});
