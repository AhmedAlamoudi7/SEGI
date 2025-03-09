$(document).ready(function () {
    //Initially load pagenumber=1
    GetPageData(1);
});
function GetPageData(pageNum, pageSize) {
    //After every trigger remove previous data and paging
    $("#userList").empty();
    $("#paged").empty();
    var selectBox = document.getElementById("selectBox");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;
    $.getJSON('/ControlPanel/AdminUser/UserList?roleTypes=' + selectedValue, { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var rowData = "";
        for (var i = 0; i < response.data.length; i++) {
            rowData = rowData + '<div class="col-md-6 col-xxl-4">\
													<div class="card">\
														<div class="card-body d-flex flex-center flex-column pt-12 p-9">\
															<div class="symbol symbol-65px symbol-circle mb-5">\
															     <img alt="avatar user" src="data:image/png;base64,'+ response.data[i].imageUrl + '">\
																<div class="bg-success position-absolute border border-4 border-body h-15px w-15px rounded-circle translate-middle start-100 top-100 ms-n3 mt-n3"></div>\
															</div>\
															<a href="/ControlPanel/AdminUser/Detailes/'+ response.data[i].id + '" class="fs-4 text-gray-800 text-hover-primary fw-bold mb-0">' + response.data[i].firstName + response.data[i].lastName + '</a>\
															<div class="fw-semibold text-gray-400 mb-6">'+ response.data[i].email + '</div>\
														</div>\
													</div>\
												</div>';
        }
        $("#userList").append(rowData);
        PaggingTemplate(response.totalPages, response.currentPage);
    });
   

        
    
}
function changeFunc(pageNum, pageSize) {
    //After every trigger remove previous data and paging
    $("#userList").empty();
    $("#paged").empty();
    var selectBox = document.getElementById("selectBox");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;
    $.getJSON('/ControlPanel/AdminUser/UserList?roleTypes=' + selectedValue, { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var rowData = "";
        for (var i = 0; i < response.data.length; i++) {
            rowData = rowData + '<div class="col-md-6 col-xxl-4">\
													<div class="card">\
														<div class="card-body d-flex flex-center flex-column pt-12 p-9">\
															<div class="symbol symbol-65px symbol-circle mb-5">\
															     <img alt="avatar user" src="data:image/png;base64,'+ response.data[i].imageUrl + '">\
																<div class="bg-success position-absolute border border-4 border-body h-15px w-15px rounded-circle translate-middle start-100 top-100 ms-n3 mt-n3"></div>\
															</div>\
															<a href="/ControlPanel/AdminUser/Detailes/'+ response.data[i].id + '" class="fs-4 text-gray-800 text-hover-primary fw-bold mb-0">' + response.data[i].firstName + response.data[i].lastName + '</a>\
															<div class="fw-semibold text-gray-400 mb-6">'+ response.data[i].email + '</div>\
														</div>\
													</div>\
												</div>';
        }
        $("#userList").append(rowData);
        PaggingTemplate(response.totalPages, response.currentPage);
    });

         }




$("#kt_filter_search").keyup(function () {
    $("#userList").empty();
    $("#paged").empty();
    $.getJSON("/ControlPanel/AdminUser/UserList?GeneralSearch=" + $('#kt_filter_search').val(), function (response) {
        var rowData = "";
        for (var i = 0; i < response.data.length; i++) {
            rowData = rowData + '<div class="col-md-6 col-xxl-4">\
													<div class="card">\
														<div class="card-body d-flex flex-center flex-column pt-12 p-9">\
															<div class="symbol symbol-65px symbol-circle mb-5">\
															     <img alt="avatar user" src="data:image/png;base64,'+ response.data[i].imageUrl + '">\
																<div class="bg-success position-absolute border border-4 border-body h-15px w-15px rounded-circle translate-middle start-100 top-100 ms-n3 mt-n3"></div>\
															</div>\
															<a href="/ControlPanel/AdminUser/Detailes/'+ response.data[i].id + '" class="fs-4 text-gray-800 text-hover-primary fw-bold mb-0">' + response.data[i].firstName + response.data[i].lastName + '</a>\
															<div class="fw-semibold text-gray-400 mb-6">'+ response.data[i].email + '</div>\
														</div>\
													</div>\
												</div>';
        }
        $("#userList").append(rowData);
        PaggingTemplate(response.totalPages, response.currentPage);
    });
});
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
    $("#paged").empty();

    template = template + '<ul class="pagination">' +
        '<li class="page-item prev"><a href="/ControlPanel/AdminUser/Index#" class="page-link" href="/ControlPanel/AdminUser/Index#" rel="prev" aria-label="Prev &raquo;" onclick="GetPageData(' + BackwardOne + ')">السابق</a></li>';

    var numberingLoop = "";
    if (PageNumberArray.length == 0) {
        numberingLoop = numberingLoop + '<div class="alert alert-warning" role="alert">لا يوجد نتائج !</div> ';
    }
    for (var i = 0; i < PageNumberArray.length; i++) {
       
  
            if (PageNumberArray[i] == CurrentPage) {
                numberingLoop = numberingLoop + ' <li class="page-item active" aria-current="page"><a style="padding-top:24px;padding-right:12px;"class="page-link" onclick="GetPageData(' + PageNumberArray[i] + ')" href="/ControlPanel/AdminUser/Index#">' + PageNumberArray[i] + ' &nbsp;&nbsp;</a></li>'
            } else {
                numberingLoop = numberingLoop + ' <li class="page-item" aria-current="page"><a style="padding-top:24px;padding-right:12px;"class="page-link" onclick="GetPageData(' + PageNumberArray[i] + ')" href="/ControlPanel/AdminUser/Index##">' + PageNumberArray[i] + ' &nbsp;&nbsp;</a></li>'      
        }
        numberingLoop;
        }
    template = template + numberingLoop + '<li class="page-item next"><a class="page-link" href="/ControlPanel/AdminUser/Index#" rel="next" aria-label="Next &raquo;" onclick="GetPageData(' + ForwardOne + ')">التالي</a></li>';
    $("#paged").append(template);

}

