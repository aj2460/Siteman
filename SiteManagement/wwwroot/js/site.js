//$(document).ready(function () {
//    $("#Title").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "/Course/Create1",
//                type: "POST",
//                datatype: "json",
//                data: { Prefix: request.term },
//                success: function (data) {
//                    response($.map(data, function (item) {
//                        return { label: item.Title, value: item.Title };
//                    }))
//                }
//            })
//        },
//        message: {
//            noResult: "", results: ""
//        }
//    });
//})



//jQuery(document).ready(function ($) {
//    $("#ExpenseTypeId").on('change', function () {
//        var level = $(this).val();
//        if (level) {
//            $.ajax(
//                {
//                    url: "/LabourExpenses/Create1",
//                    type: "POST",
//                    datatype: "json",
//                    data: { Prefix: request.term },
//                    success: function (data) {
//                        response($.map(data, function (item) {
//                            return { label: item.Title, value: item.Title };
//                        }))
//                    }
//                }

//            );
//        }
//    });
//});