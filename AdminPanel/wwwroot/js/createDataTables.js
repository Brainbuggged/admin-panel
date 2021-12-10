function createDataTables(orderProducts, orderStatus) {
    if (orderProducts) {
        OrderModelProducts = $('#OrderModelProducts').DataTable({
            "select": true,
            "order": [[1, "asc"]],
            "language": jsonLanguageDataTable
        });

      
        if (orderStatus) {

            OrderStatusChangeModel = $('#OrderStatusChangeModel').DataTable({
                "ordering": true,
                "pageLength": 3,
                "language": jsonLanguageDataTable,
            });
        }
    }
}

