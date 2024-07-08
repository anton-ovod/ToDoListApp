export const getStatusesQuery = () => {
    const query = `
        query{
            statuses{
                taskStatusID,
                taskStatusName,
                description
            }
        }
    `;
  
    return query;
  };