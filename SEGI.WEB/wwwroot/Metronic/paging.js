$(document).ready(function () {
    //Initially load pagenumber=1
    GetPageData(1);
});
function GetPageData(pageNum, pageSize) {
    //After every trigger remove previous data and paging
    $("#tblData").empty();
    $("#paged").empty();
    $.getJSON("/ControlPanel/AdminAdvisor/GetPaggedData", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var rowData = "";
        $(response).each(
            function (index, item) {
                $(item.data).each(
                    function (indexs, items) {
                        rowData = rowData + '<tr><td>' + items.id + '</td><td>' + items.firstName + '</td></tr>';

                    });
            });

        $("#tblData").append(rowData);
        PaggingTemplate(response.totalPages, response.currentPage);
    });
}
//This is paging temlpate ,you should just copy paste
function PaggingTemplate(totalPage, currentPage) {
    var template = "";
    var TotalPages = totalPage;
    var CurrentPage = currentPage;
    var PageNumberArray = Array();


    var countIncr = 1;
    for (var i = currentPage; i <= totalPage; i++) {
        PageNumberArray[0] = currentPage;
        if (totalPage != currentPage && PageNumberArray[countIncr - 1] != totalPage) {
            PageNumberArray[countIncr] = i + 1;
        }
        countIncr++;
    };
    PageNumberArray = PageNumberArray.slice(0, 5);
    var FirstPage = 1;
    var LastPage = totalPage;
    if (totalPage != currentPage) {
        var ForwardOne = currentPage + 1;
    }
    var BackwardOne = 1;
    if (currentPage > 1) {
        BackwardOne = currentPage - 1;
    }
    var numberingLoop = "";
    for (var i = 0; i < PageNumberArray.length; i++) {
        numberingLoop = numberingLoop + '<li> <a onclick="GetPageData(' + PageNumberArray[i] + ')"class="datatable-pager-link datatable-pager-link-number datatable-pager-link-active" data-page="' + PageNumberArray[i] + '" title="1">' + PageNumberArray[i] + '</a></li>';

    }
    $("#paged").empty();

    template = '<div class="datatable-pager datatable-paging-loaded">\
        <ul class="datatable-pager-nav my-2 mb-sm-0">\
            <li><a title="First" class="datatable-pager-link datatable-pager-link-first datatable-pager-link-disabled" data-page="'+ FirstPage + '" disabled="disabled" onclick="GetPageData(' + FirstPage + ')"><i class="flaticon2-fast-back"></i></a></li>\
            <li><a title="Previous" class="datatable-pager-link datatable-pager-link-prev datatable-pager-link-disabled" data-page="'+ BackwardOne + '" disabled="disabled" onclick="GetPageData(' + BackwardOne + ')" ><i class="flaticon2-back"></i></a></li>\
            <li style="display: none;"><input type="text" class="datatable-pager-input form-control" title="Page number"></li>\
            <li><a class="datatable-pager-link datatable-pager-link-number datatable-pager-link-active" data-page="1" title="1">1</a></li>\
             '+ numberingLoop + '\
            <li><a title="Next" class="datatable-pager-link datatable-pager-link-next" data-page="'+ ForwardOne + '"onclick="GetPageData(' + ForwardOne + ')"><i class="flaticon2-next"></i></a></li>\
            <li><a title="Last" class="datatable-pager-link datatable-pager-link-last" data-page="'+ LastPage + '"onclick="GetPageData(' + LastPage + ')"><i class="flaticon2-fast-next"></i></a></li>\
        </ul>\
        <div class="datatable-pager-info my-2 mb-sm-0">\
            <div class="dropdown bootstrap-select datatable-pager-size" style="width: 60px;">\
                <select class="selectpicker datatable-pager-size" id="selectedId" title="Select page size" data-width="60px" data-container="body" data-selected="20" tabindex="null">\
                    <option class="bs-title-option" value=""></option>\
                    <option value="5">5</option>\
                    <option value="10">10</option>\
                    <option value="20">20</option>\
                    <option value="30">30</option>\
                    <option value="50">50</option>\
                    <option value="100">100</option>\
                </select>\
                <button type="button" tabindex="-1" class="btn dropdown-toggle btn-light" data-toggle="dropdown" role="combobox" aria-owns="bs-select-6" aria-haspopup="listbox" aria-expanded="false" title="Select page size">\
                    <div class="filter-option">\
                        <div class="filter-option-inner">\
                            <div class="filter-option-inner-inner">20</div>\
                        </div>\
                    </div>\
                </button>\
            </div>\
            <span class="datatable-pager-detail">Showing'+ CurrentPage + '- 20 of ' + TotalPages + '</span>\
        </div>\
    </div>';


 
    $("#paged").append(template);
    $('#selectedId').change(function () {
        GetPageData(1, $(this).val());
    });
}
