// Function to show custom manual modal
function showManualModal(title, message, isSuccess = true, redirectUrl = null) {
    var modal = document.getElementById('manualModal');
    var modalTitle = document.getElementById('manualModalTitle');
    var modalMessage = document.getElementById('manualModalMessage');
    var modalIcon = document.getElementById('manualModalIcon');
    var modalBtn = document.getElementById('manualModalBtn');

    // Update Text Content
    modalTitle.textContent = title;
    modalMessage.textContent = message;

    // Update Icon & Colors
    if (isSuccess) {
        modalIcon.className = 'custom-modal-icon success';
        modalIcon.innerHTML = '<i class="fas fa-check-circle"></i>';
    } else {
        modalIcon.className = 'custom-modal-icon error';
        modalIcon.innerHTML = '<i class="fas fa-exclamation-circle"></i>';
    }

    // Show Overlay
    modal.classList.add('active');

    // Button Click Listener
    modalBtn.onclick = function () {
        modal.classList.remove('active');
        if (redirectUrl) {
            window.location.href = redirectUrl;
        }
    };
}