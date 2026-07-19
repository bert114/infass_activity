$(document).ready(function () {
    $("#registerForm").on("submit", function (e) {
        e.preventDefault();

        var $form = $(this);
        var $globalBox = $("#globalErrorBox");
        var $errorList = $("#errorList");

        // Reset old visual validation elements
        $globalBox.addClass("d-none");
        $errorList.empty();
        $(".validation-msg").text("");

        try {
            // Safely extract values and construct the JSON payload
            var payloadData = {
                Name: $("#Name").val(),
                Email: $("#Email").val(),
                Password: $("#Password").val()
            };

            var jsonString = JSON.stringify(payloadData);

            // Execute raw JSON POST request
            $.ajax({
                url: $form.attr("action"),
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: jsonString,
                dataType: "json",
                headers: {
                    "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
                },
                success: function (result) {
                    if (result.success) {
                        window.location.href = result.redirectUrl;
                    } else {
                        // Global summary errors processing
                        if (result.errors && result.errors.length > 0) {
                            $globalBox.removeClass("d-none");
                            $.each(result.errors, function (i, msg) {
                                $errorList.append($("<li>").text(msg));
                            });
                        }

                        // Field input alignment processing
                        if (result.fieldErrors) {
                            $.each(result.fieldErrors, function (key, msg) {
                                var targetSpan = $("[data-valmsg-for='" + key + "'], [asp-validation-for='" + key + "']");
                                if (targetSpan.length) {
                                    targetSpan.text(msg);
                                }
                            });
                        }
                    }
                },
                error: function (xhr, status, error) {
                    // Handles HTTP error codes (e.g. 500 Server Crashes, 404 Missing endpoints)
                    $globalBox.removeClass("d-none");
                    $errorList.append($("<li>").text("Network communication error. Please try again."));
                }
            });

        } catch (jsError) {
            // Catch native browser runtime errors (e.g., missing DOM selectors, JSON formatting errors)
            console.error("Client-side compilation error:", jsError);
            $globalBox.removeClass("d-none");
            $errorList.append($("<li>").text("An error occurred while preparing your submission details."));
        }
    });
});