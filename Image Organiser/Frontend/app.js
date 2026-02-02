const API_BASE_URL = "https://localhost:7179/api/images";
let allImages = [];


// Upload image
async function uploadImage() {
    const fileInput = document.getElementById("imageInput");
    const status = document.getElementById("status");

    if (fileInput.files.length === 0) {
        status.innerText = "Please select an image.";
        return;
    }

    const formData = new FormData();
    formData.append("file", fileInput.files[0]);

    try {
        const response = await fetch(`${API_BASE_URL}/upload`, {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            throw new Error("Upload failed");
        }

        status.innerText = "Upload successful!";
        fileInput.value = "";
        loadImages();

    } catch (error) {
        status.innerText = "Error uploading image.";
        console.error(error);
    }
}

// Load images
async function loadImages() {
    const tableBody = document.getElementById("imageTableBody");
    tableBody.innerHTML = "";

    try {
        const response = await fetch(API_BASE_URL);
        const images = await response.json();

        images.forEach(img => {
            const row = document.createElement("tr");

            row.innerHTML = `
                <td>
                    <img src="${img.url}" alt="${img.fileName}" class="thumbnail" />
                </td>
                <td>${img.fileName}</td>
                <td>${img.fileType}</td>
                <td>${img.orientation}</td>
                <td>${img.sizeInKb}</td>
                <td>${new Date(img.uploadedAt).toLocaleString()}</td>
                <td><button onclick="deleteImage(${img.id})">Delete</button></td>
            `;

            tableBody.appendChild(row);
        });

    } catch (error) {
        console.error("Failed to load images", error);
    }
}

// Load images when page loads
loadImages();

async function deleteImage(id) {
    if (!confirm("Are you sure you want to delete this image?")) return;

    try {
        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) throw new Error("Delete failed");

        // Reload images
        loadImages();

    } catch (error) {
        console.error("Error deleting image:", error);
        alert("Failed to delete image.");
    }
}

//Always load Images even on filter
async function loadImages() {
    const response = await fetch("https://localhost:7179/api/images");
    allImages = await response.json();
    renderImages(allImages);
}

function renderImages(images) {
    const tableBody = document.getElementById("imageTableBody");
    tableBody.innerHTML = "";

    images.forEach(img => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td><img src="${img.url}" class="thumbnail" /></td>
            <td>${img.fileName}</td>
            <td>${img.fileType}</td>
            <td>${img.orientation}</td>
            <td>${img.sizeInKb}</td>
            <td>${new Date(img.uploadedAt).toLocaleString()}</td>
            <td>
                <button onclick="deleteImage(${img.id})">Delete</button>
            </td>
        `;

        tableBody.appendChild(row);
    });
}

function applyFilters() {
    const search = document.getElementById("searchInput").value.toLowerCase();
    const type = document.getElementById("typeFilter").value;
    const orientation = document.getElementById("orientationFilter").value;

    const filtered = allImages.filter(img => {
        const matchesSearch = img.fileName.toLowerCase().includes(search);
        const matchesType = type === "" || img.fileType === type;
        const matchesOrientation = orientation === "" || img.orientation === orientation;

        return matchesSearch && matchesType && matchesOrientation;
    });

    renderImages(filtered);
}



