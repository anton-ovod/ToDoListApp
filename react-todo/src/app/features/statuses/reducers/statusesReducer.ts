import { Status } from '../../../../types';
import { GET_STATUSES_FAILURE, GET_STATUSES_SUCCESS } from '../actions/actionTypes';


const statusesReducer = (state: Status[] = [], action: any) : Status[]=> {
    switch(action.type){
        case GET_STATUSES_SUCCESS:
            console.log("Statuses successfully fetched!");
            return action.payload;
        case GET_STATUSES_FAILURE:
            console.log("Error while fetching categories: ", action.payload);
            return state;
        default:
            return state;
    }

};

export default statusesReducer;