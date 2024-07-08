import React, { ChangeEvent, FormEvent, useState } from 'react'
import { addTask } from '../app/features/tasks/actions/taskActions';
import { Task } from '../types';
import { RootState } from '../app/store';
import { useAppDispatch, useAppSelector } from '../app/hooks';
import { Guid } from '../misc/Guid';
import { parse } from 'date-fns';


const AddTaskForm: React.FC = () => {
    const [title, setTitle] = useState('');
    const [dueDate, setDueDate] = useState('');
    const [taskCategoryID, setTaskCategoryID] = useState('');
    const [description, setDescription] = useState('');

    const dispatch = useAppDispatch();
    const categories = useAppSelector((state: RootState) => state.categories);
    const currentStorageType = useAppSelector((state: RootState) => state.storageType);

    const handleTitleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setTitle(e.target.value);
    };

    const handleDueDateChange = (e: ChangeEvent<HTMLInputElement>) => {
        setDueDate(e.target.value);
    };

    const handleCategoryChange = (e: ChangeEvent<HTMLSelectElement>) => {
        setTaskCategoryID(e.target.value);
    };

    const handleDescriptionChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
        setDescription(e.target.value);
    };

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();

        const newTask: Task = {
            taskID: Guid.newGuid(), 
            title,
            dueDate: dueDate ? parse(dueDate, "yyyy-MM-dd'T'HH:mm", new Date()) : undefined,
            taskCategoryID: parseInt(taskCategoryID, 10) || undefined,
            description: description || undefined,
            taskStatusID: 0
        };
        dispatch(addTask(newTask, currentStorageType));

        setTitle('');
        setDueDate('');
        setTaskCategoryID('');
        setDescription('');
    };
    return (
            <form className="w-100 d-flex flex-column gap-3" onSubmit={handleSubmit}>
                <div className="container-fluid row align-items-center">
                    <label className="fs-3 w-25" htmlFor="title">Title</label>
                    <input className="w-75 h-100 fs-3" 
                        id="title" 
                        name="title" 
                        value={ title } 
                        onChange={ handleTitleChange } />
                </div>
                <div className="container-fluid row align-items-center">
                    <label className="fs-3 w-25" htmlFor="dueDate">Due Date</label>
                    <input
                        type="datetime-local"
                        className="w-auto h-100 fs-4"
                        id="dueDate"
                        name="dueDate"
                        value={ dueDate }
                        onChange={  handleDueDateChange }
                    />
                    <label className="fs-4 w-auto mx-3" htmlFor="taskCategoryID">Category</label>
                    <select
                        id="taskCategoryID"
                        name="taskCategoryID"
                        className="w-auto form-select fs-4"
                        value={ taskCategoryID }
                        onChange={  handleCategoryChange }
                    >
                        <option value="">Choose Category</option>
                        {categories.map(category => (
                        <option key={category.taskCategoryID} value={category.taskCategoryID}>
                            {category.taskCategoryName}
                        </option>
                    ))}
                    </select>
                </div>
                <div className="container-fluid row align-items-center">
                    <label className="fs-3 w-25 align-self-start" htmlFor="description">Description</label>
                    <textarea
                        className="w-75 fs-4"
                        id="description"
                        name="description"
                        rows={5}
                        value={ description }
                        onChange={ handleDescriptionChange } ></textarea>
                </div>
                <div className="container-fluid row align-items justify-content-center center">
                    <button type="submit" className="btn btn-primary w-25 fs-4">Add Task</button>
                </div>
            </form>
    )
}

export default AddTaskForm
