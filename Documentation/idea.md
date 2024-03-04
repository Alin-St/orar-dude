# Orar Dude

App that retrieves a timetable for a given group from Babeș-Bolyai University (CS), and displays it in a more readable format than the original. The intended source format is *Format Tabelar*, examples [here](https://www.cs.ubbcluj.ro/files/orar/2023-2/tabelar/index.html).

The app itself should be a **WinForms desktop application**. The user-friendly timetable should be generated as a **.html** (similar to the original).

Features should be optional, and incrementally added to the core app. The user can enable more features, for more data processing, or less features for a more compatible app.

## Features:

1. [Base program] Download a webpage from a given link, and display the html.
    * Provided information:
        * Link with the tabular timetable ([example](https://www.cs.ubbcluj.ro/files/orar/2023-2/tabelar/IE3.html))
2. Isolate a given group from the rest of the timetables.
3. Parse the (first) html table into a custom matrix format. Build the html file manually using the custom matrix format.
4. Parse the custom matrix format into a custom object format. Each row is now an object, with properties for each column in the table. Build the html using the custom object format.
5. Highlight days, make it clear what coures are on each day.
    * Merge cells with the same day together. Example: All *Monday* cells arre merged together.
    * Outline with a thicker border the group of rows on the same day.
6. Highlight subjects / labs that the student is not interested in.
    * The following rows will have a red background:
        * If the student is not enrolled in the course
        * If the student is not part of the target subgroup (ex: lab is for 936/1 and student is in 936/2)
    * Provided information:
        * A list of blacklisted courses
        * A list of whitelisted courses
        * 
