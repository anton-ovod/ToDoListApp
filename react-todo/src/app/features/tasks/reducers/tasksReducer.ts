import { Task } from '../../../../types';
import { GET_TASKS_SUCCESS, ADD_TASK_SUCCESS, DELETE_TASK_SUCCESS, UPDATE_TASK_SUCCESS, CHANGE_STATUS_SUCCESS, GET_TASKS_FAILURE, ADD_TASK_FAILURE, DELETE_TASK_FAILURE, UPDATE_TASK_FAILURE, CHANGE_STATUS_FAILURE } from '../actions/actionTypes';


const tasksReducer = (state: Task[] = [], action: any) : Task[] => {
    switch (action.type) {
        case GET_TASKS_SUCCESS:
            console.log("Tasks fetched sucessfully!");
            return action.payload;
        case GET_TASKS_FAILURE:
            console.log("Error while fetching tasks: ", action.payload);
            return state;
        case ADD_TASK_SUCCESS:
            return [...state, action.payload];
        case ADD_TASK_FAILURE:
            console.log(action.payload);
            return state;
        case DELETE_TASK_SUCCESS:
            return state.filter(task => task.taskID.toString() != action.payload);
        case DELETE_TASK_FAILURE:
            console.log(action.payload);
            return state;
        case UPDATE_TASK_SUCCESS:
            return state.map(task => 
                task.taskID.toString() == action.payload.taskID ? action.payload : task
            );
        case UPDATE_TASK_FAILURE:
            console.log(action.payload);
            return state;
        case CHANGE_STATUS_SUCCESS:
            return state.map(task => 
                task.taskID.toString() == action.payload ? { ...task, taskStatusID: task.taskStatusID + 1 } : task
            );
        case CHANGE_STATUS_FAILURE:
            console.log(action.payload);
            return state;
        default:
            return state;
    }
};

export default tasksReducer;
