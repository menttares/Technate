


$('.course-panel__button').on('click', function () {
    $('.course-panel__button').removeClass('active'); // Убираем класс active у всех кнопок
    $(this).addClass('active'); // Добавляем класс active только к нажатой кнопке

    var page = $(this).attr("data-pages");
    $.ajax({
        url:   `/Course/${page}/${codeCourse}`,
        method: 'GET', // или 'POST' или другой метод HTTP
        dataType: 'html', // тип ожидаемых данных (json, html, xml и т.д.)
        success: function (data) {
            // Обработка успешного ответа от сервера
            renderView(data);
            
        },
        error: function (error) {
            // Обработка ошибок
            console.log('Произошла ошибка:', error);
            renderView('');
        }
    });

});


function renderView(page) {
    var contentBody = $('#courese-content');

    contentBody.html(page);

}