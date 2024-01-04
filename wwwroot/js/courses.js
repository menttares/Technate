
$('#Menu__button-create-course').on("click", function(){
    $("#myModal").addClass("active");
});

function closeModal() {
    $("#myModal").removeClass("active");
}

$(document).ready(function() {
    $("#submitBtn").click(function() {
        // Получение данных из полей формы
        var courseName = $("#courseName").val();
        var courseSubject = $("#courseSubject").val();

        // Ваша логика обработки данных, например, отправка на сервер
        var data = {
            courseName: courseName, 
            courseSubject, courseSubject
        }

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
        
        closeModal();
    });
});