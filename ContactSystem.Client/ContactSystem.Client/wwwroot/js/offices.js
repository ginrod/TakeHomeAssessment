document.addEventListener("DOMContentLoaded", async () => {
    const dropdown = document.getElementById("officesDropdown");
    const baseUrl = dropdown.dataset.api;

    try {
        const apiResult = await fetch(`${baseUrl}/api/v1/Offices/getOffices`);

        if (!apiResult.ok)
            throw new Error("Failed to fetch offices");

        const result = await apiResult.json();

        console.log("Raw Api results" + result);

        const offices = result.data;

        console.log("Offices: " + JSON.stringify(offices));

        offices.forEach(office => {
            const option = document.createElement("option");

            option.value = office.id;
            option.textContent = office.name;

            dropdown.appendChild(option);
        });
    }
    catch (error) {
        console.error("Error loading offices:", error);

        const errorDiv = document.getElementById("errorMessage");
        errorDiv.textContent = "Failed to load offices. Please try again later.";
    }

    dropdown.addEventListener("change", () => {
        const officeId = dropdown.value;
        const officeName = dropdown.options[dropdown.selectedIndex].text;

        if (officeId) {
            console.log(`Redirecting to Contact Pages`);
            window.location.href = `/Contacts?officeId=${officeId}&officeName=${encodeURIComponent(officeName)}`
        }
        else {
            console.log("Could not redirect to Contacts after office selection");
        }
    });
});