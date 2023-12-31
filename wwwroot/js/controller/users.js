﻿var users = {
    Init: function () {
        users.LoadData();
        users.RegisterEvent();
    },
    RegisterEvent: function () {
        $("#btnSearch").off('click').on('click', function () {
            users.LoadData();
            users.RegisterEvent();
        })
        $(".page-link").off('click').on('click', function () {
            users.LoadData($(this).text());
            users.RegisterEvent();
        })
        $(".delete-user").off('click').on('click', function () { 
            var url = $(this).attr('src');
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: 'https://localhost:7034'+url,
                        type: 'GET',
                        success: function (response) {
                            Swal.fire(
                                'Deleted!',
                                'User has been deleted.',
                                'success'
                            ).then(function () {
                                users.LoadData();
                            })

                        },
                        error: function (xhr, status, error) {
                            console.log(xhr);
                            console.log(error);
                        }
                    });
                  
                }
            })

        })
    },
    LoadData: function (pageLink) {
        var keyword = $("#search").val();
        var index = pageLink ?? 1;
        var size = 4
        var url = `/CustomUser/GetUser?keyword=${keyword}&index=${index}&size=${size}`

        $('#content').empty();
        $('#pageList').empty();
        $.get(url, function (response) {
            console.log(response);
            var totalRow = response.totalRow;
            var pages = (totalRow / size);
            if (totalRow % size > 0) {
                pages += 1;
            }
            var html = '';
            $.each(response.users, function (i, value) {
                var row = '<tr>' +
                    '<td>' +
                    '<div class="media align-items-center">' +
                    '<img class="me-3 img-fluid rounded-circle" width="75" src="/' + value.avatar + '">' +
                    '</div>' +
                    '</td>' +
                    '<td>' + value.name + '</td>' +
                    '<td class="wspace-no">' + value.gender + '</td>' +
                    '<td>' + value.age + '</td>' +
                    '<td>' +
                    '<div class="dropdown ms-auto c-pointer">' +
                    '<div class="btn-link" data-bs-toggle="dropdown">' +
                    '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">' +
                    '<path d="M11.0005 12C11.0005 12.5523 11.4482 13 12.0005 13C12.5528 13 13.0005 12.5523 13.0005 12C13.0005 11.4477 12.5528 11 12.0005 11C11.4482 11 11.0005 11.4477 11.0005 12Z" stroke="#3E4954" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />' +
                    '<path d="M18.0005 12C18.0005 12.5523 18.4482 13 19.0005 13C19.5528 13 20.0005 12.5523 20.0005 12C20.0005 11.4477 19.5528 11 19.0005 11C18.4482 11 18.0005 11.4477 18.0005 12Z" stroke="#3E4954" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />' +
                    '<path d="M4.00049 12C4.00049 12.5523 4.4482 13 5.00049 13C5.55277 13 6.00049 12.5523 6.00049 12C6.00049 11.4477 5.55277 11 5.00049 11C4.4482 11 4.00049 11.4477 4.00049 12Z" stroke="#3E4954" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />' +
                    '</svg>' +
                    '</div>' +
                    '<div class="dropdown-menu dropdown-menu-end">' +
                    '<a class="dropdown-item text-info update-user" href="/Admin/UpdateUser/' + value.id + '" >' +
                    '<svg class="me-2" width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">' +
                    '<path d="M22 11.08V12C21.9988 14.1564 21.3005 16.2547 20.0093 17.9818C18.7182 19.709 16.9033 20.9725 14.8354 21.5839C12.7674 22.1953 10.5573 22.1219 8.53447 21.3746C6.51168 20.6273 4.78465 19.2461 3.61096 17.4371C2.43727 15.628 1.87979 13.4881 2.02168 11.3363C2.16356 9.18457 2.99721 7.13633 4.39828 5.49707C5.79935 3.85782 7.69279 2.71538 9.79619 2.24015C11.8996 1.76491 14.1003 1.98234 16.07 2.86" stroke="#2F4CDD" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />' +
                    '<path d="M22 4L12 14.01L9 11.01" stroke="#2F4CDD" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />' +
                    '</svg>' +
                    'Update' +
                    '</a>' +
                    '<a class="dropdown-item text-danger delete-user" src="/CustomUser/DeleteUser/?id=' + value.id + '" >' +
                    '<svg class="me-2" width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">' +
                    '<path d="M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" stroke="#F24242" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>' +
                    '<path d="M15 9L9 15" stroke="#F24242" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>' +
                    '<path d="M9 9L15 15" stroke="#F24242" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>' +
                    '</svg>' +
                    'Delete' +
                    '</a>' +
                    '</div>' +
                    '</div>' +
                    '</td>' +
                    '</tr>'
                    ;
                html += row;
            })
            $('#content').append(html);

            var pageNumber = 1;
            var pageList = '<li class="page-item page-indicator ">' +
                '<button class="page-link">' +
                '<i class="la la-angle-left"></i>' +
                '</button>' +
                '</li>';
            do {
                pageList += '<li class="page-item">'
                    + '<button class="page-link">' + pageNumber + '</button>'
                    + '</li>';
                pageNumber++;
            } while (pageNumber <= pages);
            pageList += '<li class="page-item page-indicator">' +
                '<button class="page-link" >' +
                '<i class="la la-angle-right"></i>' +
                '</button>' +
                '</li>';
            $('#pageList').append(pageList);
            $("#pageList button.page-link").filter(function () {
                return $(this).text() === pageLink;
            }).closest('li').addClass("active");

            users.RegisterEvent();
        })
    }
}

users.Init();