import { legacy_createStore as createStore, combineReducers, applyMiddleware, compose } from 'redux';
import tasksReducer from './features/tasks/reducers/tasksReducer';
import categoriesReducer from './features/categories/reducers/categoriesReducer';
import statusesReducer from './features/statuses/reducers/statusesReducer';
import storageTypeReducer from './features/storageType/storageTypeReducer';
import { combineEpics, createEpicMiddleware, Epic } from 'redux-observable';
import { addTaskEpic, changeStatusEpic, deleteTaskEpic, getTasksEpic, updateTaskEpic } from './features/tasks/epics/taskEpics';
import { getCategoriesEpic } from './features/categories/epics/categoryEpics';
import { RootAction } from '../types';
import { getStatusesEpic } from './features/statuses/epics/statusesEpics';



const rootReducer = combineReducers({
  tasks: tasksReducer,
  categories: categoriesReducer,
  statuses: statusesReducer,
  storageType: storageTypeReducer,
});

const rootEpic: Epic<RootAction, RootAction, void, any> = combineEpics<RootAction, RootAction, void, any>(
  getTasksEpic,
  addTaskEpic,
  deleteTaskEpic,
  updateTaskEpic,
  changeStatusEpic,
  getCategoriesEpic,
  getStatusesEpic,
);

const epicMiddleware = createEpicMiddleware<RootAction, RootAction, void, any>();

const store = createStore(
  rootReducer,
  applyMiddleware(epicMiddleware),
);

epicMiddleware.run(rootEpic);

export default store;

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch
