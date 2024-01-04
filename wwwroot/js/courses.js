
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

        // Закрыть всплывающее окно после отправки данных
        closeModal();
    });
});