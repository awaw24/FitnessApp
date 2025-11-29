const RenderClientTrainings = (trainings, container) => {
    container.empty();

    for (const training of trainings) {
        container.append(
            `<div class="card border-secondary mb-3" style="max-width: 18rem;">
          <div class="card-header">${training.date}</div>
          <div class="card-body">
            <h5 class="card-title">${training.description}</h5> 
          </div>
        </div>`)
    }
}

const LoadClientTrainings = () => {
    const container = $("#trainings")
    const clientEncodedName = container.data("encodedName");

    $.ajax({
        url: `/Client/${clientEncodedName}/ClientTraining`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no trainings for this client")
            } else {
                RenderClientTrainings(data, container)
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}
