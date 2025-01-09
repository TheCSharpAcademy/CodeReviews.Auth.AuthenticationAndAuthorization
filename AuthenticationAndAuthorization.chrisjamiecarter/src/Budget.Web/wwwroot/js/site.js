$('#dynamicModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var url = button.data('url');
    var modal = $(this);

    $.get(url, function (data) {
        modal.find('.modal-body').html(data);
        // Re-initialize jQuery Unobtrusive Validation
        $.validator.unobtrusive.parse(modal.find('form'));
    });
});

$('#dynamicModal').on('submit', 'form', function (e) {
    // Prevent default form submission.
    e.preventDefault(); 

    var form = $(this);
    var url = form.attr('action');
    var method = form.attr('method');

    $.ajax({
        url: url,
        type: method,
        data: form.serialize(),
        success: function (response) {
            // Check if validation errors exist by inspecting the returned response.
            if ($(response).find('.validation-summary-errors').length > 0 ||
                $(response).find('.field-validation-error').length > 0) {
                // If there are validation errors, replace the modal content with the returned form.
                $('#dynamicModal .modal-body').html($(response)); 
            } else {
                // If the form submission was successful (no validation errors)
                $('#dynamicModal').modal('hide');
                // Refresh the page to update the transaction list
                location.reload();
            }
        },
        error: function () {
            alert("An error occurred. Please try again.");
        }
    });
});
