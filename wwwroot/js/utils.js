function showManualModal(title, message, isSuccess = true, redirectUrl = null) {
    var modal = document.getElementById('manualModal');
    var modalTitle = document.getElementById('manualModalTitle');
    var modalMessage = document.getElementById('manualModalMessage');
    var modalIcon = document.getElementById('manualModalIcon');
    var modalBtn = document.getElementById('manualModalBtn');

    modalTitle.textContent = title;
    modalMessage.textContent = message;

    if (isSuccess) {
        modalIcon.className = 'custom-modal-icon success';
        modalIcon.innerHTML = '<i class="fas fa-check-circle"></i>';
    } else {
        modalIcon.className = 'custom-modal-icon error';
        modalIcon.innerHTML = '<i class="fas fa-exclamation-circle"></i>';
    }

    modal.classList.add('active');

    modalBtn.onclick = function () {
        modal.classList.remove('active');
        if (redirectUrl) {
            window.location.href = redirectUrl;
        }
    };
}