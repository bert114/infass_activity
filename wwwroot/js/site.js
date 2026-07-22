function showModal(title, message, isSuccess = true, redirectUrl = null) {
    // Set text
    $('#themeModalLabel').text(title);
    $('#themeModalMessage').text(message);

    // Dynamic icon styling
    if (isSuccess) {
        $('#modalIcon').attr('class', 'fas fa-check-circle text-theme-teal');
    } else {
        $('#modalIcon').attr('class', 'fas fa-exclamation-circle text-danger');
    }

    // Show modal
    var modalInstance = new bootstrap.Modal(document.getElementById('themeModal'));
    modalInstance.show();

    // Handle button action or redirect
    $('#btnModalConfirm').off('click').on('click', function () {
        if (redirectUrl) {
            window.location.href = redirectUrl;
        }
    });
}