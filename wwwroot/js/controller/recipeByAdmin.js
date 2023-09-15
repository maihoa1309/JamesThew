var recipeByAdmin =
{
    Init: function () {
        recipeByAdmin.RegisterEvent();
        recipeByAdmin.LoadData();
    },
    RegisterEvent: function () {
        $("#btnSearch").off('click').on('click', function () {
            recipeByAdmin.LoadData();
        })
        $("li.page-item").off('click').on('click', function () {
            console.log("da click");
        /*    recipeByAdmin.PageList();*/
        })

    },
    LoadData: function () { 
        var keyword = $("#search").val();
        var index = 1;
        var size = 5
        var url = `/Recipe/GetByName?keyword=${keyword}&index=${index}&size=${size}`
        
        $.get(url, function (response) {
  /*          console.log(response);*/
            var totalRow = response[0].totalRow;
            console.log(totalRow);
            var html = '';
            var pages = (totalRow / size);
            if (totalRow % size > 0) {
                pages += 1;
            }
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
                    ;
                html += row;
            });
            $('#content').append(html);
            var pageList = '<li class="page-item page-indicator">' +
                '<a class="page-link" href="javascript:void(0)">' +
                '<i class="la la-angle-left"></i>' +
                '</a>' +
                '</li>';
            do {
                pageList += '<li class="page-item">'
                                + '<a class="page-link">' + index + '</a >'
                            + '	</li > ';
                index++;
            } while (index <= pages);
            pageList += '<li class="page-item page-indicator">' +
                '<a class="page-link" href="javascript:void(0)">' +
                '<i class="la la-angle-right"></i>' +
                '</a>' +
                '</li>';
            $('#pageList').append(pageList);
        })

     
        recipeByAdmin.RegisterEvent();
    },
    PageList: function () {
        var index = $(".page-link").text();
        console.log(index);

        recipeByAdmin.RegisterEvent();
    }
}
recipeByAdmin.Init();