export const getCategoriesQuery = () => {
    const query = `
      query {
        categories {
          taskCategoryID
          taskCategoryName
          description
        }
      }
    `;
  
    return query;
  };