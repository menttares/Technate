
$('.Menu__button-create').on("click", function () {
    $("#myModal").addClass("active");
});

function closeModal() {
    $("#myModal").removeClass("active");
}


$('.Menu__join-button').on('click', function () {
    $("#modalCode").show();
});

$("#closeModalBtn").click(function () {
    $("#modalCode").hide();
});



function closeModalCode() {
    $("#modalCode").removeClass("active");
}

$('#button-show-create-coureses').on('click', function () {

    $.ajax({
        url: '/Home/GetRetrieveManagedCourses',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Обрабатываем полученные данные (data)
            console.log(data);
            createCourseElements(data);
        },
        error: function () {
            // Обрабатываем ошибку
            console.error('Ошибка при запросе курсов');
        }
    });


});



$('#button-show-my-coureses').on('click', function () {
    $.ajax({
        url: '/Home/GetCoursesUser',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Обрабатываем полученные данные (data)
            console.log(data);
            createCourseElements(data);
        },
        error: function () {
            // Обрабатываем ошибку
            console.error('Ошибка при запросе курсов');
        }
    });
});

$('#button-show-coureses-archive').on('click', function () {

});



function createCourseElements(courses) {
    // Получаем контейнер, в который будем добавлять курсы
    var coursesContainer = document.getElementById('container-courses-id');

    coursesContainer.innerHTML = "";


    // Проходим по массиву курсов и создаем для каждого курса HTML-элемент
    courses.forEach(function (course) {
        // Создаем элемент курса
        var courseElement = document.createElement('div');
        courseElement.className = 'courses__course-block';

        // Добавляем внутренности курса
        courseElement.innerHTML = `
            <div class="courses__course-avatar">
            <svg xmlns="http://www.w3.org/2000/svg" width="79" height="71" viewBox="0 0 79 71" fill="none">
            <g clip-path="url(#clip0_95_73)">
              <path d="M70.9886 58.3897H7.35523C3.2996 58.3897 0 55.0901 0 51.0344V7.35522C0 3.30011 3.2996 0 7.35523 0H57.8613C58.5818 0 59.1658 0.584005 59.1658 1.30449C59.1658 2.02497 58.5818 2.60898 57.8613 2.60898H7.35523C4.73851 2.60898 2.60899 4.73851 2.60899 7.35522V51.0344C2.60899 53.6511 4.73851 55.7807 7.35523 55.7807H70.9891C73.6059 55.7807 75.7354 53.6511 75.7354 51.0344V7.35522C75.7354 4.73851 73.6059 2.60898 70.9891 2.60898H65.852C65.1315 2.60898 64.5475 2.02497 64.5475 1.30449C64.5475 0.584005 65.1315 0 65.852 0H70.9891C75.0448 0 78.3444 3.2996 78.3444 7.35522V51.0344C78.3439 55.0901 75.0443 58.3897 70.9886 58.3897Z" fill="#231F20"/>
              <path d="M51.9398 70.5905H26.4041C24.9028 70.5905 23.6818 69.3695 23.6818 67.8682V67.1009C23.6818 65.5997 24.9028 64.3786 26.4041 64.3786H51.9398C53.441 64.3786 54.6631 65.5997 54.6631 67.1009V67.8682C54.6636 69.369 53.4415 70.5905 51.9398 70.5905ZM26.4041 66.9866C26.3412 66.9866 26.2902 67.0375 26.2902 67.1004V67.8677C26.2902 67.9306 26.3412 67.9816 26.4041 67.9816H51.9398C52.0026 67.9816 52.0547 67.9306 52.0547 67.8677V67.1004C52.0547 67.0375 52.0026 66.9866 51.9398 66.9866H26.4041Z" fill="#231F20"/>
              <path d="M47.9501 66.2852H30.3948C29.6743 66.2852 29.0903 65.7011 29.0903 64.9807V57.1784C29.0903 56.4579 29.6743 55.8739 30.3948 55.8739H47.9501C48.6706 55.8739 49.2546 56.4579 49.2546 57.1784V64.9807C49.2546 65.7011 48.6706 66.2852 47.9501 66.2852ZM31.6993 63.6761H46.6456V58.4829H31.6993V63.6761Z" fill="#231F20"/>
              <path d="M71.6839 46.5606H6.66048C5.94 46.5606 5.35599 45.9766 5.35599 45.2562V6.9453C5.35599 6.22481 5.94 5.64081 6.66048 5.64081H71.6839C72.4044 5.64081 72.9884 6.22481 72.9884 6.9453V45.2562C72.9884 45.9766 72.4038 46.5606 71.6839 46.5606ZM7.96498 43.9522H70.3794V8.25031H7.96498V43.9522Z" fill="#231F20"/>
              <path d="M70.9886 58.3897H7.35523C3.2996 58.3897 0 55.0901 0 51.0344V49.8984C0 49.1779 0.584008 48.5939 1.30449 48.5939H77.0399C77.7604 48.5939 78.3444 49.1779 78.3444 49.8984V51.0344C78.3439 55.0901 75.0443 58.3897 70.9886 58.3897ZM2.61156 51.2034C2.70066 53.7428 4.79413 55.7812 7.35471 55.7812H70.9886C73.5497 55.7812 75.6427 53.7428 75.7318 51.2034H2.61156Z" fill="#231F20"/>
            </g>
            <defs>
              <clipPath id="clip0_95_73">
                <rect width="78.3444" height="70.5905" fill="white"/>
              </clipPath>
            </defs>
          </svg>
            </div>
            <div class="courses__description">
                <div class="courses__title-block">
                    <div class="courses__course-title">
                        <a  class="courses__title-link" href="/Course/Course/${course.courseCode}">${course.courseName}</a>
                    </div>
                    <div class="course__course-counter-people-block">
                        <div class="course__course-svg-people">
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 25 25" fill="none">
                        <path d="M10.9375 21.875C10.9375 21.875 9.375 21.875 9.375 20.3125C9.375 18.75 10.9375 14.0625 17.1875 14.0625C23.4375 14.0625 25 18.75 25 20.3125C25 21.875 23.4375 21.875 23.4375 21.875H10.9375ZM17.1875 12.5C18.4307 12.5 19.623 12.0061 20.5021 11.1271C21.3811 10.248 21.875 9.0557 21.875 7.8125C21.875 6.5693 21.3811 5.37701 20.5021 4.49794C19.623 3.61886 18.4307 3.125 17.1875 3.125C15.9443 3.125 14.752 3.61886 13.8729 4.49794C12.9939 5.37701 12.5 6.5693 12.5 7.8125C12.5 9.0557 12.9939 10.248 13.8729 11.1271C14.752 12.0061 15.9443 12.5 17.1875 12.5Z" fill="#263238"/>
                        <path fill-rule="evenodd" clip-rule="evenodd" d="M8.15 21.875C7.91837 21.3872 7.80285 20.8524 7.8125 20.3125C7.8125 18.1953 8.875 16.0156 10.8375 14.5C9.85795 14.1982 8.83742 14.0506 7.8125 14.0625C1.5625 14.0625 0 18.75 0 20.3125C0 21.875 1.5625 21.875 1.5625 21.875H8.15Z" fill="#263238"/>
                        <path d="M7.03125 12.5C8.06725 12.5 9.06082 12.0884 9.79339 11.3559C10.5259 10.6233 10.9375 9.62975 10.9375 8.59375C10.9375 7.55775 10.5259 6.56418 9.79339 5.83161C9.06082 5.09905 8.06725 4.6875 7.03125 4.6875C5.99525 4.6875 5.00168 5.09905 4.26911 5.83161C3.53655 6.56418 3.125 7.55775 3.125 8.59375C3.125 9.62975 3.53655 10.6233 4.26911 11.3559C5.00168 12.0884 5.99525 12.5 7.03125 12.5Z" fill="#263238"/>
                      </svg>
                        </div>
                        <div class="course__count-people">
                            ${course.studentsCount}
                        </div>
                    </div>
                </div>
                <div class="courses__description-container">
                    ${course.courseSubject}
                </div>
                <div class="courses__info">
                    <div class="courses__info-owner">
                        ${course.creatorUsername}
                    </div>
                    <div class="courses__info-date-create">
                        ${course.dateCreate}
                    </div>
                </div>
            </div>
        `;

        // Добавляем созданный элемент курса в контейнер
        coursesContainer.appendChild(courseElement);
    });
}



$("#joinBtn").click(function () {
    var courseCode = $("#codeInput").val();

    $.ajax({
        type: 'POST',
        url: '/Course/InviteCourse', // Укажите URL вашего метода на сервере, который будет обрабатывать аутентификацию
        data: courseCode,
        contentType: 'application/json',  // Добавьте эту строку
        success: function (response) {
            // Обработка успешного ответа от сервера
            console.log(response);
            window.location.href = `/Course/Course/${courseCode}`;

        },
        error: function (error) {
            // Обработка ошибки
            console.log(error.responseText);
        }
    });

    $("#modalCode").hide();
});


$("#CreateBtn").click(function () {
    var Name = $("#courseName").val();
    var Subject = $("#courseSubject").val();

    var CourseViewModel = {
        courseName: Name,
        courseSubject: Subject
    }

    $.ajax({
        type: 'POST',
        url: '/Course/CreateCourse', // Укажите URL вашего метода на сервере, который будет обрабатывать аутентификацию
        data: JSON.stringify(CourseViewModel),
        contentType: 'application/json',  // Добавьте эту строку
        success: function (response) {
            // Обработка успешного ответа от сервера
            console.log(response);
            window.location.href = `/Course/Course/${response}`;
        },
        error: function (error) {
            // Обработка ошибки
            console.log(error.responseText);
        }
    });

    $("#myModal").removeClass("active");
});



