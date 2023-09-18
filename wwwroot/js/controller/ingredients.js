
var ingredients =
{
    Init: function () {
        ingredients.LoadData();
        ingredients.RegisterEvent();
    },
    RegisterEvent: function ()
    {
        $("#btnSearch").off('click').on('click', function () {
            ingredients.LoadData();
            alert("clicked");
            ingredients.RegisterEvent();
        })
        $(".page-link").off('click').on('click', function () {
            ingredients.LoadData($(this).text());
            alert("clicked");
            ingredients.RegisterEvent();
        })
        //$(".linkDelete").off('click').on('click'), function () {

        //    alert("clicked");
        //    ingredients.RegisterEvent();
        //   /* /Ingredient/DeleteIngredient /*/
        //}
    },
    LoadData: function (pageLink) {
        var keyword = $("#search").val();
        var index = pageLink ?? 1;
        var size = 15
        var url = `/Ingredient/GetByName?keyword=${keyword}&index=${index}&size=${size}`

        $('#content').empty();
        $('#pageList').empty();
        $.get(url, function (response) {
            //console.log(response)
            console.log(response[0].deletedUser);
            var totalRow = response[0].deletedUser;
            var pages = (totalRow / size);
            if (totalRow % size > 0) {
                pages += 1;
            }
            var html = '';
            $.each(response, function (i, v) {
                var row = '<div class="row align-items-center p-4  border-bottom">' +
                    '<div class="col-10">' +
                    v.name +
                    '</div>' +
                    '<div class="col-1 offset-1">' +
                    '<a href="/Ingredient/DeleteIngredient/' + v.id + '" class="linkDelete">' +
                    '<svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">' +
                    '<path d="M16.0007 1.33333C13.0999 1.33333 10.2642 2.19351 7.85229 3.80511C5.44037 5.4167 3.56051 7.70732 2.45042 10.3873C1.34034 13.0673 1.04989 16.0163 1.61581 18.8613C2.18172 21.7064 3.57859 24.3197 5.62976 26.3709C7.68093 28.4221 10.2943 29.8189 13.1393 30.3849C15.9844 30.9508 18.9334 30.6603 21.6133 29.5502C24.2933 28.4402 26.584 26.5603 28.1955 24.1484C29.8071 21.7365 30.6673 18.9008 30.6673 16C30.6627 12.1116 29.116 8.38371 26.3665 5.63416C23.617 2.88462 19.8891 1.33792 16.0007 1.33333ZM16.0007 28C13.6273 28 11.3072 27.2962 9.33381 25.9776C7.36042 24.6591 5.82235 22.7849 4.9141 20.5922C4.00585 18.3995 3.76821 15.9867 4.23123 13.6589C4.69426 11.3311 5.83715 9.19295 7.51538 7.51472C9.19361 5.83649 11.3318 4.6936 13.6596 4.23057C15.9873 3.76755 18.4001 4.00519 20.5929 4.91344C22.7856 5.82169 24.6597 7.35977 25.9783 9.33316C27.2969 11.3065 28.0007 13.6266 28.0007 16C27.9971 19.1815 26.7317 22.2317 24.482 24.4814C22.2324 26.7311 19.1822 27.9965 16.0007 28Z" fill="#F44C62" />' +
                    '<path d="M21.6556 10.3427C21.4055 10.0927 21.0665 9.95228 20.7129 9.95228C20.3594 9.95228 20.0203 10.0927 19.7702 10.3427L15.9996 14.1147L12.2289 10.3427C12.1059 10.2153 11.9588 10.1137 11.7961 10.0439C11.6334 9.97399 11.4585 9.9372 11.2814 9.93567C11.1044 9.93413 10.9288 9.96786 10.765 10.0349C10.6011 10.1019 10.4522 10.2009 10.327 10.3261C10.2019 10.4513 10.1029 10.6002 10.0358 10.7641C9.96877 10.9279 9.93504 11.1035 9.93657 11.2805C9.93811 11.4576 9.97489 11.6325 10.0448 11.7952C10.1147 11.9579 10.2162 12.105 10.3436 12.228L14.1142 16L10.3436 19.772C10.2162 19.895 10.1147 20.0421 10.0448 20.2048C9.97489 20.3675 9.93811 20.5424 9.93657 20.7195C9.93504 20.8965 9.96877 21.0721 10.0358 21.2359C10.1029 21.3998 10.2019 21.5487 10.327 21.6739C10.4522 21.7991 10.6011 21.8981 10.765 21.9651C10.9288 22.0321 11.1044 22.0659 11.2814 22.0643C11.4585 22.0628 11.6334 22.026 11.7961 21.9561C11.9588 21.8863 12.1059 21.7847 12.2289 21.6573L15.9996 17.8853L19.7702 21.6573C19.8932 21.7847 20.0404 21.8863 20.203 21.9561C20.3657 22.026 20.5407 22.0628 20.7177 22.0643C20.8947 22.0659 21.0703 22.0321 21.2342 21.9651C21.398 21.8981 21.5469 21.7991 21.6721 21.6739C21.7973 21.5487 21.8963 21.3998 21.9633 21.2359C22.0304 21.0721 22.0641 20.8965 22.0626 20.7195C22.061 20.5424 22.0243 20.3675 21.9544 20.2048C21.8845 20.0421 21.7829 19.895 21.6556 19.772L17.8849 16L21.6556 12.228C21.9055 11.978 22.046 11.6389 22.046 11.2853C22.046 10.9318 21.9055 10.5927 21.6556 10.3427Z" fill="#F44C62" />' +
                    '</svg>' +
                    '</a>' +
                    '</div>' +
                    '</div>'
                    ;
                html += row;
            })
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

            ingredients.RegisterEvent();
        })
    }
}
ingredients.Init();