// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    let tSwiftTitles = [];

    // Load the Taylor Swift songs from the .txt file
    function loadSongTitles() {
        return $.get('/TSwift.txt').then(function (data) {
            tSwiftTitles = data.split('\n').map(title => title.trim()).filter(title => title);
        });
    }

    // Utility function to get a random song title and ensure uniqueness
    function getRandomTitle(usedTitles) {
        if (tSwiftTitles.length === 0) return "Untitled";

        let availableTitles = tSwiftTitles.filter(title => !usedTitles.has(title));
        if (availableTitles.length === 0) return "Untitled"; // Fallback if all titles used

        let randomIndex = Math.floor(Math.random() * availableTitles.length);
        let selectedTitle = availableTitles[randomIndex];
        usedTitles.add(selectedTitle); // Keep track of used titles

        return selectedTitle;
    }

    // Event handler for button click to replace card titles
    $('#assignNames').on('click', function () {
        loadSongTitles().then(function () {
            let usedTitles = new Set(); // Track used song titles to avoid duplicates

            // Iterate through each card title and assign a random song title
            $('.card-title').each(function () {
                let newTitle = getRandomTitle(usedTitles);
                $(this).text(newTitle);
            });
        });
    });
});