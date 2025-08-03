let page = 1;
const pageSize = 5;
let totalPages = 1;

async function searchContacts() {

    const searchContactsInput = document.getElementById("searchContactsInput");
    const term = searchContactsInput.value;
    const baseUrl = searchContactsInput.dataset.api;
    const officeId = searchContactsInput.dataset.officeId;

    console.log("Office Id: " + officeId);

    try {
        const apiResult = await fetch(`${baseUrl}/api/v1/Contacts/getContacts?name=${encodeURIComponent(term)}&officeId=${officeId}&page=${page}&pageSize=${pageSize}`);

        const result = await apiResult.json();

        console.log("Raw Api results" + result);

        const contacts = result.data;
        totalPages = Math.ceil(result.totalRecords / pageSize);


        console.log("Contacts: " + JSON.stringify(contacts));

        renderTable(contacts);

        document.getElementById("paginationControls").style.display = "block";
        document.getElementById("pageIndicator").textContent = page;
        document.getElementById("totalPages").textContent = totalPages;
    }
    catch (error) {
        console.error("Error searching contacts:", error);

        const errorDiv = document.getElementById("errorMessage");
        errorDiv.textContent = "Unable to retrieve contacts at the moment. Please try again later.";
    }
}

async function findAllContacts() {

    const searchContactsInput = document.getElementById("searchContactsInput");
    const baseUrl = searchContactsInput.dataset.api;
    const officeId = searchContactsInput.dataset.officeId;

    console.log("Office Id: " + officeId);

    try {
        const apiResult = await fetch(`${baseUrl}/api/v1/Contacts/getAllContacts?officeId=${officeId}`);

        const result = await apiResult.json();

        console.log("Raw Api results" + result);

        const contacts = result.data;

        console.log("Contacts: " + JSON.stringify(contacts));

        renderTable(contacts);
    }
    catch (error) {
        console.error("Error searching contacts:", error);

        const errorDiv = document.getElementById("errorMessage");
        errorDiv.textContent = "Unable to retrieve contacts at the moment. Please try again later.";
    }
}

function renderTable(contacts) {
    const resultsDiv = document.getElementById("results");

    if (!contacts || contacts.length === 0) {
        resultsDiv.textContent = "No contacts found.";

        document.getElementById("paginationControls").style.display = "none";
    }

    const table = document.createElement("table");
    table.style.borderCollapse = "collapse";
    table.classList.add("table", "table-bordered", "table-striped", "w-100");

    // Header row
    const header = document.createElement("tr");
    const columnNames = ["First Name", "Last Name", "Email"];

    columnNames.forEach(text => {
        const th = document.createElement("th");
        th.textContent = text;

        th.style.border = "1px solid #ccc";
        th.style.padding = "8px";
        header.appendChild(th)
    });

    table.appendChild(header);

    // Data rows
    contacts.forEach(c => {
        const tr = document.createElement("tr");

        const contactDetails = [c.firstName, c.lastName, c.email];

        contactDetails.forEach(detail => {
            const td = document.createElement("td");

            td.textContent = detail;
            td.style.border = "1px solid #ccc";
            td.style.padding = "8px";
            tr.appendChild(td);
        });
        table.appendChild(tr);
    });

    resultsDiv.innerHTML = "";
    resultsDiv.appendChild(table);
}

function nextPage() {
    if (page < totalPages) {
        ++page;
        searchContacts();
    }
}

function previousPage() {
    if (page > 1) {
        --page;
        searchContacts();
    }
}