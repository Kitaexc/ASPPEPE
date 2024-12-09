document.addEventListener('DOMContentLoaded', function () {
    const toggleBtn = document.getElementById('toggle-btn');
    const authForm = document.getElementById('auth-form');
    const registerForm = document.getElementById('register-form');
    const toggleTitle = document.getElementById('toggle-title');
    const toggleDescription = document.getElementById('toggle-description');

    toggleBtn.addEventListener('click', function () {
        authForm.classList.toggle('d-none');
        registerForm.classList.toggle('d-none');

        if (authForm.classList.contains('d-none')) {
            toggleTitle.textContent = 'Уже есть аккаунт?';
            toggleDescription.textContent = 'Авторизуйтесь, чтобы войти на сайт.';
            toggleBtn.textContent = 'Авторизация';
        } else {
            toggleTitle.textContent = 'Еще нет аккаунта?';
            toggleDescription.textContent = 'Зарегистрируйтесь, чтобы получить доступ к эксклюзивным функциям нашего сайта.';
            toggleBtn.textContent = 'Регистрация';
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const registerForm = document.querySelector('form[action="/Account/Register"]');
    const loginForm = document.querySelector('form[action="/Account/Login"]');

    function handleFormSubmit(form, event) {
        event.preventDefault();

        const formData = new FormData(form);
        fetch(form.action, {
            method: form.method,
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    if (data.redirectUrl) {
                        window.location.href = data.redirectUrl; // Перенаправление
                    } else {
                        showNotification(data.message, "success");
                    }
                } else {
                    showNotification(data.message, "error");
                }
            })
            .catch(() => {
                showNotification("Произошла ошибка. Попробуйте снова.", "error");
            });
    }

    if (registerForm) {
        registerForm.addEventListener('submit', event => handleFormSubmit(registerForm, event));
    }

    if (loginForm) {
        loginForm.addEventListener('submit', event => handleFormSubmit(loginForm, event));
    }

    function showNotification(message, type) {
        const notification = document.createElement('div');
        notification.className = `notification ${type}`;
        notification.textContent = message;
        document.body.appendChild(notification);

        setTimeout(() => {
            notification.remove();
        }, 3000);
    }
});
