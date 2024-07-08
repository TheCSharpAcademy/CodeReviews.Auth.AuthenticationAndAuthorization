// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deletePatient(id) {
    fetch(`/Patient/DeletePatient/${id}`, {
        method: 'POST'
    })
        .then(response => {
            window.location.href = '/Patient/Index';
        })
}

function deleteExam(id,patientId) {
    fetch(`/Exam/DeleteExam/${id}`, {
        method: 'POST'
    }).then(response => {
        window.location.href = `/Patient/PatientDetail/${patientId}`;
    })
}

function deleteExamFromExamView(id) {
    fetch(`/Exam/DeleteExam/${id}`, {
        method: 'POST'
    }).then(response => {
        window.location.href = `/Exam/Index`;
    })
}

function openAddPatientPage() {
    var partialpage = document.getElementById("addpatientcontainer")
    partialpage.style.display='block'

}

function closeAddPatientPage() {
    var partialpage = document.getElementById("addpatientcontainer")
    partialpage.style.display = 'none'

}

function openaddexampage() {
    var partialpage = document.getElementById("addexamcontainer")
    partialpage.style.display = 'block'
    var button = document.getElementById("addexamclosebutton")
    button.style.display='inline'
}

function closeaddexampage() {
    var partialpage = document.getElementById("addexamcontainer")
    partialpage.style.display = 'none'
    var button = document.getElementById("addexamclosebutton")
    button.style.display = 'none'
}

