import React, { useState } from 'react'
import {format} from 'date-fns';
import { Task, Category, Status } from '../types';
import dateImg from '../assests/images/date.svg';
import deleteImg from '../assests/images/delete.svg';
import pencilImg from '../assests/images/pencil.svg';
import TaskUpdateForm from './TaskUpdateForm';
import { useAppDispatch, useAppSelector } from '../app/hooks';
import { RootState } from '../app/store';
import { deleteTask, changeStatus, updateTask } from '../app/features/tasks/actions/taskActions';

const TaskListing: React.FC<{task: Task} > = ({task}) => {
  const[showDescription, setShowDescription] = useState(false);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  
  const categories = useAppSelector((state: RootState) => state.categories);
  const statuses = useAppSelector((state: RootState) => state.statuses);
  const currentStorageType = useAppSelector((state: RootState) => state.storageType);
  const dispatch = useAppDispatch();

  const handleDelete = () => {
    dispatch(deleteTask(task.taskID.toString(), currentStorageType));
  };

  const handleChangeStatus = () => {
    dispatch(changeStatus(task.taskID.toString(), currentStorageType));
  };

  const updateFormWrapperHandler = (updatedTask: Task) => {
    setShowUpdateForm(false);
    dispatch(updateTask(updatedTask, currentStorageType));
  };
  return (
    <div key={task.taskID.toString()} id={`id-${task.taskID.toString()}`} className="container-fluid d-flex px-0 border border-1 rounded-2 border-secondary" style={{ height: 'auto', minHeight: '80px' }}>
    <div className="container-fluid row px-0 mx-0 align-items-center" style={{display: !showUpdateForm ? 'flex' : 'none'}}>
        <div className="fs-4 m-0 col text-center" style={{ cursor: 'pointer' }} onClick={ () => setShowDescription((prevState) => !prevState)}>
            {task.title}
        </div>
        <div className="fs-4 col text-center">
            {categories.find(category => category.taskCategoryID == task.taskCategoryID)?.taskCategoryName}
        </div>
        <div className="fs-4 col d-flex flex-row gap-1 justify-content-center align-items-center">
            {task.dueDate && (
                <>
                    <div className="d-flex align-items-center justify-content-center">
                        <img src={dateImg} alt="Date" width="35" />
                    </div>
                    <div className="text-center">
                        {format(task.dueDate, 'dd.MM.yyy HH:mm')}
                    </div>
                </>
            )}
        </div>
        <div className="col text-center">
              {task.taskStatusID != 2 && (
                  <div className="btn border-0 fs-4" onClick={(e) => { e.preventDefault(); setShowUpdateForm(true) }}>
                      <img src={pencilImg} alt="Edit" width="25" />
                  </div>
              )}

            <div className="btn border-0 fs-4" onClick={ handleDelete }>
                <img src={deleteImg} alt="Delete" width="25" />
            </div>
        </div>
        <div className="col align-self-stretch px-0 text-center d-flex justify-content-center align-items-center">
            {
                task.taskStatusID == 2 ? 
                (<div className="bg-success fs-4 w-100 h-100 rounded-0 d-flex align-items-center justify-content-center"
                    style={{color: 'white'}}
                  onClick={task.taskStatusID !== 2 ? handleChangeStatus : undefined}>
                {statuses.find(status => status.taskStatusID == task.taskStatusID)?.taskStatusName}
                </div>) : 
                (
                    <div className="btn btn-primary fs-4 w-100 h-100 rounded-0 d-flex align-items-center justify-content-center"
                    onClick={task.taskStatusID !== 2 ? handleChangeStatus : undefined}>
                    {statuses.find(status => status.taskStatusID == task.taskStatusID)?.taskStatusName}
                    </div>
                )

            }
        </div>
        {task.description && (
            <div className="container row border-primary border-top border-3 mx-0 description my-2"
            style={{display: showDescription ? 'flex' : 'none'}}>
                <span className="fs-3 fw-bold">Description: </span>
                <p className="fs-4" style={{ textAlign: 'justify' }}>{task.description}</p>
            </div>
        )}
    </div>
    {task.taskStatusID != 2 && 
    <div className="container-fluid row mx-0 align-items-center py-3" style={{display: showUpdateForm ? 'flex' : 'none'}}>
        <TaskUpdateForm task={ task } updateTask={updateFormWrapperHandler} />
    </div>
    }

    </div>
  )
}

export default TaskListing;

