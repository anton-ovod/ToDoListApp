import React, { useEffect, useLayoutEffect, useState } from 'react'
import TaskListing from './TaskListing'
import { useAppDispatch, useAppSelector } from '../app/hooks'
import { RootState } from '../app/store'
import { getTasks } from '../app/features/tasks/actions/taskActions'
import Spinner from './Spinner'
import { transformWithEsbuild } from 'vite'

const TaskListings: React.FC<{title: string }> = ({ title }) => {
    const currentStorageType = useAppSelector((state: RootState) => state.storageType);
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    const tasks = useAppSelector((state: RootState) => state.tasks);

    return (
    <>
    <p className="fs-2 align-self-start m-0">{ title }: </p>
    {title == "Current tasks" ? 
        <div className="container-fluid pe-0 align-items-center row fw-bold">
        <div className="fs-4 m-0 col text-center">Title: </div>
        <div className="fs-4 m-0 col text-center">Category: </div>
        <div className="fs-4 m-0 col text-center">DueDate: </div>
        <div className="fs-4 m-0 col text-center"></div>
        <div className="fs-4 m-0 col text-center"></div>
        </div> : ''
    }
    {!Array.isArray(tasks) ? <Spinner loading={true} /> :
    tasks.filter(task => title == "Current tasks" ? task.taskStatusID != 2 : task.taskStatusID == 2).map(task => (
        <TaskListing  key={task.taskID.toString()} task={task} /> ))}

    </>
  )
}

export default TaskListings;
