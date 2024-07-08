import { createRoot } from "react-dom/client"
import App from "./App"
import { Provider } from 'react-redux';
import store from "./app/store";
import "./index.css"
import { getTasks } from "./app/features/tasks/actions/taskActions";
import { getCategories } from "./app/features/categories/actions/categoryActions";
import { getStatuses } from "./app/features/statuses/actions/statusActions";

const container = document.getElementById("root")

store.dispatch(getTasks("SQL"));
store.dispatch(getCategories("SQL"));
store.dispatch(getStatuses("SQL"));

if (container) {
  const root = createRoot(container)


  root.render(
      <Provider store={store}>
          <App />
      </Provider>
  )
} else {
  throw new Error(
    "Root element with ID 'root' was not found in the document. Ensure there is a corresponding HTML element with the ID 'root' in your HTML file.",
  )
}
