(function () {

    function initial() {
        registerEvents();
    }

    function registerEvents() {
        <script>
            $(document).ready(function () {
                $('.input-notes').change(function () {
                    var foodId = $(this).data('food-id');
                    var notes = $(this).val();

                    $.ajax({
                        url: '/ControllerName/SaveNotes',
                        method: 'POST',
                        data: { foodId: foodId, notes: notes },
                        success: function (response) {
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                });
    });
        </script>
    }
    initial();

})();