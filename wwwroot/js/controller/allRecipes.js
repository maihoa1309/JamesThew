var recipeByAdmin =
{
    Init: function () {
        recipeByAdmin.RegisterEvent();
        recipeByAdmin.LoadData();
    },
    RegisterEvent: function () {
        $("#btnSearch").off('click').on('click', function () {
            recipeByAdmin.LoadData();
            recipeByAdmin.RegisterEvent();
        })
        $(".page-link").off('click').on('click', function () {

            //var index = $(this).text();

            //alert("on click" + index);
            recipeByAdmin.LoadData($(this).text());
            recipeByAdmin.RegisterEvent();
        })
    },
    LoadData: function (pageLink) { 
        var keyword = $("#search").val();
        var index = pageLink ?? 1;
        /*  var index = 1;*/
        console.log(index);
        console.log(keyword);
        var size = 5
        var url = `/Recipe/GetByName?keyword=${keyword}&index=${index}&size=${size}`

        $('#content').empty();
        $('#pageList').empty();
        
        $.get(url, function (response) {
         /*   console.log(response);*/
            var totalRow = response[0].totalRow;
            var pages = (totalRow / size);
            if (totalRow % size > 0) {
                pages += 1;
            }
            var html = '';
            $.each(response, function (i, v) {
               
                var row = '<tr>'
                    + '<td style="width: 45%">'
                    + '<div class="media align-items-center">'
                    + '<img class="me-3 img-fluid rounded-circle" width="75" src="/' + v.recipe.img.split(',')[0] + '">'
                    + '<div class="media-body">'
                    + '<h5 class="mt-0 mb-2"><a href="ecom-product-detail.html" class="text-black">' + v.recipe.title + '</a></h5>'
                    + '<p class="mb-0 text-primary" >' + v.recipe.id + '</p >'
                    + ' </div>'
                    + ' </div >'
                    + '</td > '
                    + '<td style="width: 40%">'
                    + '<h5 class="mb-2 text-black wspace-no">' + v.user.name + '</h5>'
                    + '</td>'
                    + '<td style="width: 10%">'
                    + '<div class="d-flex align-items-center">'
                    + '<a class="btn btn-danger light btn-sm" href="/Admin/AddOrUpdateRecipe/' + v.recipe.id + '">See detail</a>'
                    + '</div>'
                    + '</td>'
                    + '</tr>';
                     
                html += row;
            });
            $('#content').append(html);
            
            var pageNumber = 1;
            var pageList = '<li class="page-item page-indicator">' +
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


            recipeByAdmin.RegisterEvent();

        })

    }
   
}
recipeByAdmin.Init();