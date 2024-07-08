import React, { useEffect } from "react"
import Header from "./components/Header";
import Footer from "./components/Footer";
import StorageTypeForm from "./components/StorageTypeForm";
import AddTaskForm from "./components/AddTaskForm";
import TaskListings from "./components/TaskListings";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./App.css"


const App: React.FC = () => {

    return (
        <>
        <Header headerTitle = {"ToDoListApplication"} />
        <div className="container">
            <div className=" mb-3 d-flex justify-content-center flex-column align-items-center gap-3">
                <div className="d-flex px-2 py-4 justify-content-center align-items-center border border-2 rounded-2 border-primary flex-column" style={{width: '90%'}}>
                        <StorageTypeForm />
                        <AddTaskForm />
                </div>
            </div>
            <div className="  mb-3 container d-flex flex-column gap-4 px-4 py-4 justify-content-center align-items-center border border-2 rounded-2 border-primary" style={{width:'90%'}}>
                <TaskListings title="Current tasks" />
            </div>
            <div className=" container d-flex flex-column gap-4 px-4 py-4 justify-content-center align-items-center border border-2 rounded-2 border-primary" style={{width:'90%'}}>
            <TaskListings title="Finished tasks" />
            </div>
        </div>
        <Footer />
        </>
    )
}

export default App

