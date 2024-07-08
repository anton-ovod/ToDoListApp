export const getTasksQuery = `
  query {
    tasks {
      taskID
      title
      dueDate
      taskCategoryID
      taskStatusID
      description
    }
  }
`;

export const addTaskQuery = (
    title: string, 
    description?: string, 
    taskCategoryID?: number,
    dueDate?: Date
  ) => {
    const query = `
      mutation {
        addTask(task: {
          title: "${title}",
          ${description ? `description: "${description}",` : ''}
          ${taskCategoryID ? `taskCategoryID: ${taskCategoryID},` : ''}
          ${dueDate ? `dueDate: "${dueDate.toISOString()}",` : ''}
        }) 
      }
    `;
  
    return query;
};

export const deleteTaskQuery = (id: string) => {
  const query = `
    mutation {
      deleteTask(id: "${id}")
    }
  `;

  return query;
}

export const updateTaskQuery = (
  id: string,
  title?: string,
  description?: string,
  dueDate?: Date,
  taskCategoryID?: number
) => {
  const query = `
    mutation {
      updateTask(id: "${id}", task: {
          title: "${title}",
          ${description ? `description: "${description}",` : ''}
          ${taskCategoryID ? `taskCategoryID: ${taskCategoryID},` : ''}
          ${dueDate ? `dueDate: "${dueDate.toISOString()}",` : ''}
        })
    }
  `;

  return query;
};

export const changeStatusQuery = (id: string) => {
  const query = `
    mutation {
      changeStatus(id: "${id}") 
    }
  `;

  return query;
};