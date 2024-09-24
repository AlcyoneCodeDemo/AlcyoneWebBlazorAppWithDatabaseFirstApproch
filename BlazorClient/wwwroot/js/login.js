var Login = {
    attachEvents: function () {
        console.log('Attaching events');
        $("#loginFormBtn").off('click').on('click', function () {
            Login.submitForm();
        });
    },

    submitForm: function () {
        if ($("#myForm").valid()) {
            Login.PerformRegistration();
        } else {
            console.log('Form is not valid');
        }
    },

    PerformRegistration: function () {
        var password = $('#login-password').val(); // Use val() to get the password value
        var email_address = $('#login-email').val();

        var Model = {
            password: password,
            email: email_address
        };

        $.ajax({
            type: "POST",
            url: '/api/users/PutUser',
            contentType: 'application/json', // Set content type to JSON
            data: JSON.stringify(Model), // Convert the Model object to a JSON string
            success: function (result, status, xhr) {
                try {
                    if (result.statusCode == 200) {
                        console.log('Registration successful');
                        $("#dashboard").hide();
                    } else {
                        console.log('Info', result.responseMessage);
                    }
                } catch (error) {
                    console.error('Error parsing JSON:', error);
                }
            },
            error: function (xhr, status, error) {
                console.error("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
            }
        });
    },

    validatePortalExistence: function () {
        $.ajax({
            type: "GET",
            url: '/api/users/GetUsers',
            data: { "Id": 1 },
            success: function (result, status, xhr) {
                if (result.statusCode == 200) {
                    if (!result.isExists) {
                        console.log('User does not exist, please register');
                    } else {
                        Login.attachEvents();
                        console.log('Login successful!');
                        window.location.href = '/dashboard';
                    }
                } else {
                    console.error("Error", result.responseMessage);
                }
            },
            error: function (xhr, status, error) {
                console.error("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
            }
        });
    }
};

$(document).ready(function () {
    Login.validatePortalExistence();
});
