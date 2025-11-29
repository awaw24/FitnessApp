$(document).ready(function () {

    LoadClientTrainings();
    $("#createClientTrainingModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("New training added")
                LoadClientTrainings();
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });
});