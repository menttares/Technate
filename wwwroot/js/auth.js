function Registration() {
    var username = $('.auth-form__input[name="username"]').val();
    var email = $('.auth-form__input[name="email"]').val();
    var password = $('.auth-form__input[name="password"]').val();

    // Подготовка данных для отправки на сервер
    var RegisterViewModel = {
        username: username,
        email: email,
        password: password
    };

    $.ajax({
        type: 'POST',
        url: '/Login/Registration', // Укажите URL вашего метода на сервере, который будет обрабатывать аутентификацию
        data: JSON.stringify(RegisterViewModel),
        contentType: 'application/json',  // Добавьте эту строку
        success: function (response) {
            // Обработка успешного ответа от сервера
            console.log(response);
            // Дополнительные действия, например, перенаправление на другую страницу
            window.location.href = '/Home/Index';
        },
        error: function (error) {
            // Обработка ошибки
            console.log(error.responseText);
            // Дополнительные действия при ошибке
        }
    });
}

function Authorization() {
    var email = $('.auth-form__input[name="email"]').val();
    var password = $('.auth-form__input[name="password"]').val();

    // Подготовка данных для отправки на сервер
    var LoginViewModel = {
        email: email,
        password: password
    };

    $.ajax({
        type: 'POST',
        url: '/Login/Authorization', // Укажите URL вашего метода на сервере, который будет обрабатывать аутентификацию
        data: JSON.stringify(LoginViewModel),
        contentType: 'application/json',  // Добавьте эту строку
        success: function (response) {
            // Обработка успешного ответа от сервера
            console.log(response);
            window.location.href = '/Home/Index';
        },
        error: function (error) {
            // Обработка ошибки
            console.log(error.responseText);
            // Дополнительные действия при ошибке
        }
    });
}