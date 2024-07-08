import React, { useState } from 'react'
import { Status, Task, Category} from '../types'
import {format, parse} from 'date-fns';
import { useAppSelector } from '../app/hooks';
import { RootState } from '../app/store';

const TaskUpdateForm: React.FC<{task: Task, updateTask: Function }> = ({task, updateTask}) => {
  const [title, setTitle] = useState<string>(task.title);
  const [dueDate, setDueDate] = useState<string>(
    task.dueDate ? format(new Date(task.dueDate), "yyyy-MM-dd'T'HH:mm") : ""
  );
  const [categoryID, setCategoryID] = useState<number | "">(task.taskCategoryID || "");
  const [description, setDescription] = useState<string>(task.description || "");

  const categories = useAppSelector((state: RootState) => state.categories);

  const submitForm = (e: React.FormEvent) =>{
    e.preventDefault();

    const updatedTask : Task = {
      ...task,
      title,
      dueDate: dueDate ? parse(dueDate,"yyyy-MM-dd'T'HH:mm", new Date()) : undefined,
      taskCategoryID: parseInt(categoryID.toString(), 10) || undefined,
      description,
    }
    updateTask(updatedTask);
  }
  return (
    <form className="container-fluid d-flex flex-column gap-2 align-self-stretch" onSubmit={submitForm}>
    <div className="container-fluid d-flex flex-row gap-3 align-items-center align-self-stretch px-0">
        <input className="col m-0 fs-4" 
                id="title" 
                name="title" 
                value={title} 
                onChange={(e) => {setTitle(e.target.value)}} />
        <select
                id="taskCategoryID"
                name="taskCategoryID"
                className="col form-select fs-4 m-0"
                value={categoryID}
                onChange={(e) => {setCategoryID(parseInt(e.target.value))}}>
                <option value="">Choose Category</option>
                {categories.map(category => (
                    <option key={category.taskCategoryID} value={category.taskCategoryID}>{category.taskCategoryName}</option>
                ))}
        </select>
        <input
              type="datetime-local"
              className="col fs-4 h-auto w-auto m-0"
              id="dueDate"
              name="dueDate"
              value={dueDate}
              onChange={(e) => {setDueDate(e.target.value)}} />
        <input type="submit" className="btn btn-primary text-center fs-5 m-0 col h-100" value="Apply Changes"/>
    </div>
    <div className="container-fluid d-flex flex-column gap-3 align-items-center align-self-stretch px-0">
        <textarea
                  className="fs-4 w-100"
                  id="description"
                  name="description"
                  rows={5}
                  value={description}
                  onChange={(e) => {setDescription(e.target.value)}}>

        </textarea>
    </div>
</form>


  )
}

export default TaskUpdateForm
