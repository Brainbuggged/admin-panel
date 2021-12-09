function createDataTables(invCnt, orderProducts, invCnt_Tsk_Lns, refreshHeaders) {
    //if (invCnt) {
    //    if (refreshHeaders) {
    //        $('#InventoryCounts thead tr').clone(true).appendTo('#InventoryCounts thead');
    //        var inc = 0;
    //        $('#InventoryCounts thead tr:eq(1) th').each(function (i) {
    //            $(this).prop("id", inc);
    //            var title = $(this).text();
    //            $(this).html('<input type="text" style="text-align: center;" placeholder="' + title + '" />');
    //            inc++;

    //            $('input', this).on('keyup change', function () {
    //                if (inventoryCounts.column(i).search() !== this.value) {
    //                    if (this.value.substring(this.value.length - 1) == "*") {
    //                        var strBeforeAsterix = this.value.substring(0, this.value.length - 1);
    //                        inventoryCounts.column(i).search("^" + strBeforeAsterix + "(.)*$", true, false).draw();
    //                    }
    //                    else if (this.value.substring(0, 1) == "*") {
    //                        var strAfterAsterix = this.value.substring(1);
    //                        inventoryCounts.column(i).search("^(.)*" + strAfterAsterix + "$", true, false).draw();
    //                    }
    //                    else {
    //                        inventoryCounts.column(i).search(this.value).draw();
    //                        //if (title.trim() == "Номер волны" || title.trim() == "Зона" && this.value.length > 0) {
    //                        //    var searchTerm = this.value.toLowerCase();
    //                        //    inventoryCounts.column(i).search("^" + searchTerm + "$", true, false).draw();
    //                        //}
    //                        //else {
    //                        //    inventoryCounts.column(i).search(this.value).draw();
    //                        //}
    //                    }
    //                }
    //            });
    //        });
    //    }

    //    inventoryCounts = $('#orders').DataTable({
    //        "processing": true,
    //        "columns": [
    //            { "name": "GUID" },
    //            { "name": "CountNum" },
    //            { "name": "Zone" },
    //            { "name": "Status" },
    //            { "name": "Locations" },
    //            { "name": "WaveNumber" },
    //            { "name": "CounterType" }
    //        ],
    //        "filter": true,
    //        "searching": true,
    //        "ordering": false,
    //        "select": true,
    //        "pageLength": 10,
    //        "dom": 'Bflrtip',
    //        "buttons": [
    //            "colvis"
    //        ],
    //        "columnDefs": [
    //            {
    //                "className": "dt-center",
    //                "targets": "_all",
    //            },
    //            {
    //                "targets": [ 0, 1 ],
    //                "visible": false,
    //                "orderable": false
    //            }
    //        ],
    //        "language": jsonLanguageDataTable,
    //        "orderCellsTop": true,
    //        "fixedHeader": true,
    //        //"order": [[5, "asc"], [2, "asc"], [3, "asc"]],
    //        "stateSave": true
    //    });

    //    var state = inventoryCounts.state.loaded();
    //    if (state) {
    //        inventoryCounts.columns().eq(0).each(function (colIdx) {
    //            var colSearch = state.columns[colIdx].search;
    //            if (colSearch.search) {
    //                $('input', $('#InventoryCounts thead tr:eq(1) th[id=' + colIdx + ']')).val(colSearch.search);
    //            }
    //        });
    //        inventoryCounts.draw();
    //    }
    //}

    if (orderProducts) {
        //if (refreshHeaders) {
        //    $('#OrderModelProducts thead tr').clone(true).appendTo('#OrderModelProducts thead');
        //    inc = 0;
        //    $('#OrderModelProducts thead tr:eq(1) th').each(function (i) {
        //        $(this).prop("id", inc);
        //        var title = $(this).text();
        //        $(this).html('<input type="text" style="text-align: center;" placeholder="' + title + '" />');
        //        inc++;
        //        $('input', this).on('keyup change', function () {
        //            if (OrderModelProducts.column(i).search() !== this.value) {
        //                OrderModelProducts.column(i).search(this.value).draw();
        //            }
        //        });
        //    });
        //}

        OrderModelProducts = $('#OrderModelProducts').DataTable({
            "select": true,
            "order": [[1, "asc"]],
            "dom": 'Bflrtip',
            "columnDefs": [
                {
                    "className": "dt-center",
                    "targets": "_all",
                },
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ],
            "language": jsonLanguageDataTable,
            "orderCellsTop": true,
            "fixedHeader": true,
            "stateSave": true
        });

        //    state = OrderModelProducts.state.loaded();
        //    if (state) {
        //        InventoryCount_Tasks.columns().eq(0).each(function (colIdx) {
        //            var colSearch = state.columns[colIdx].search;
        //            if (colSearch.search) {
        //                $('input', $('#InventoryCount_Tasks thead tr:eq(1) th[id=' + colIdx + ']')).val(colSearch.search);
        //            }
        //        });
        //        InventoryCount_Tasks.draw();
        //    }
        //}
        //if (invCnt_Tsk_Lns) {
        //    if (refreshHeaders) {
        //        $('#InventoryCountTasks_Lines thead tr').clone(true).appendTo('#InventoryCountTasks_Lines thead');
        //        inc = 0;
        //        $('#InventoryCountTasks_Lines thead tr:eq(1) th').each(function (i) {
        //            $(this).prop("id", inc);
        //            var title = $(this).text();
        //            $(this).html('<input type="text" style="text-align: center;" placeholder="' + title + '" />');
        //            inc++;

        //            $('input', this).on('keyup change', function () {
        //                if (InventoryCountTasks_Lines.column(i).search() !== this.value) {
        //                    if (InventoryCountTasks_Lines.column(i).search() !== this.value) {
        //                        if (this.value.substring(this.value.length - 1) == "*") {
        //                            var strBeforeAsterix = this.value.substring(0, this.value.length - 1);
        //                            InventoryCountTasks_Lines.column(i).search("^" + strBeforeAsterix + "(.)*$", true, false).draw();
        //                        }
        //                        else if (this.value.substring(0, 1) == "*") {
        //                            var strAfterAsterix = this.value.substring(1);
        //                            InventoryCountTasks_Lines.column(i).search("^(.)*" + strAfterAsterix + "$", true, false).draw();
        //                        }
        //                        else {
        //                            if (title.trim() == "Зона" && this.value.length > 0) {
        //                                var searchTerm = this.value.toLowerCase();
        //                                InventoryCountTasks_Lines.column(i).search("^" + searchTerm + "$", true, false).draw();
        //                            }
        //                            else {
        //                                InventoryCountTasks_Lines.column(i).search(this.value).draw();
        //                            }
        //                        }
        //                    }
        //                }
        //            });
        //        });
        //    }

        //    InventoryCountTasks_Lines = $('#InventoryCountTasks_Lines').DataTable({
        //        "ordering": true,
        //        "pageLength": 10,
        //        "dom": 'Bflrtip',
        //        "buttons": [
        //            "colvis"
        //        ],
        //        "columnDefs": [
        //            {
        //                "className": "dt-center",
        //                "targets": "_all",
        //            }
        //        ],
        //        "language": jsonLanguageDataTable,
        //        "orderCellsTop": true,
        //        "fixedHeader": true,
        //        "stateSave": true
        //    });
        //    state = InventoryCountTasks_Lines.state.loaded();
        //    if (state) {
        //        InventoryCountTasks_Lines.columns().eq(0).each(function (colIdx) {
        //            var colSearch = state.columns[colIdx].search;
        //            if (colSearch.search) {
        //                $('input', $('#InventoryCountTasks_Lines thead tr:eq(1) th[id=' + colIdx + ']')).val(colSearch.search);
        //            }
        //        });
        //        InventoryCountTasks_Lines.draw();
        //    }
        //}
    }
}

