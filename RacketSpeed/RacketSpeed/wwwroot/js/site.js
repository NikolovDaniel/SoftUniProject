﻿document.addEventListener("DOMContentLoaded", function () {
    // Get references to the select elements and the span element
    const playersSelect = document.getElementById("peopleCount");
    const racketsSelect = document.getElementById("racketsCount");
    const totalPriceDisplayInput = document.getElementById("totalPriceDisplay");
    const totalPriceReservationInput = document.getElementById("totalPriceReservation");

    // Calculate the price based on your pricing logic
    let playerPrice = parseInt(playersSelect.value) * 5;  // Assuming 5 leva per player
    let racketPrice = parseInt(racketsSelect.value) * 2;   // Assuming 2 leva per racket
    totalPriceDisplayInput.value = `${playerPrice + racketPrice} лв.`;
    totalPriceReservationInput.value = playerPrice + racketPrice;


    // Add event listeners to the select elements
    playersSelect.addEventListener("change", calculatePrice);
    racketsSelect.addEventListener("change", calculatePrice);

    // Function to calculate the price based on the selected options
    function calculatePrice() {
        const selectedPlayers = parseInt(playersSelect.value);
        const selectedRackets = parseInt(racketsSelect.value);

        // Calculate the price based on your pricing logic
        const playerPrice = selectedPlayers * 5;  // Assuming 5 leva per player
        const racketPrice = selectedRackets * 2;   // Assuming 2 leva per racket

        const totalPrice = playerPrice + racketPrice;

        // Update the input element with the calculated price
        totalPriceDisplayInput.value = `${totalPrice} лв.`;
        totalPriceReservationInput.value = playerPrice + racketPrice;
    }
});

document.addEventListener("DOMContentLoaded", populateHours());
// Populate the available hours based on the selected court and date
function populateHours() {
    // Get the selected court and date values
    var courtId = $("#courtSelect").val();
    var selectedDate = $("#dateSelect").val();

    // Make an AJAX request to retrieve the available hours
    $.ajax({
        url: "/Booking/GetAvailableHours", // Replace with your server endpoint
        method: "GET",
        data: { courtNumber: courtId, date: selectedDate },
        success: function (response) {
            // Clear the existing options in the hour select tag
            $("#hourSelect").empty();

            // Populate the hour select tag with the available hours
            response.forEach(function (schedule) {
                $("#hourSelect").append(
                    $("<option>", {
                        value: schedule.hour,
                        text: schedule.hour,
                    })
                );
            });
        },
        error: function (error) {
            console.log("Error retrieving available hours: " + error);
        },
    });
}

// Event listener for court select change
$("#courtSelect").change(function () {
    populateHours();
});

// Event listener for date select change
$("#dateSelect").change(function () {
    populateHours();
});

//document.addEventListener("DOMContentLoaded", function () {
//    var dateSelect = document.getElementById("dateSelect");
//    var hourSelect = document.getElementById("hourSelect");

//    dateSelect.addEventListener("change", function () {
//        var selectedDate = dateSelect.value;

//        // Make an AJAX request to the server to retrieve available hours for the selected date
//        var xhr = new XMLHttpRequest();
//        xhr.open("GET", "/Booking/getAvailableHours?date=" + selectedDate, true);

//        xhr.onreadystatechange = function () {
//            if (xhr.readyState === XMLHttpRequest.DONE) {
//                if (xhr.status === 200) {
//                    var response = JSON.parse(xhr.responseText);

//                    // Clear the existing options
//                    hourSelect.innerHTML = "";

//                    // Get the available hours for the selected court
//                    var selectedCourtId = courtSelect.value;
//                    var availableHours = response[selectedCourtId];

//                    // Populate the hour options
//                    for (var i = 0; i < availableHours.length; i++) {
//                        var hour = availableHours[i];
//                        var option = document.createElement("option");
//                        option.value = hour;
//                        option.textContent = hour;
//                        hourSelect.appendChild(option);
//                    }
//                } else {
//                    // Handle error
//                    console.log(xhr.responseText);
//                }
//            }
//        };

//        xhr.send();
//    });
//});