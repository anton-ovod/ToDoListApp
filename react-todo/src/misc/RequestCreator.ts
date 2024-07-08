import { StorageType } from "../types";

export const createRequest = (query: string, storageType: StorageType) => {
  const request = {
    url: 'https://localhost:7052/graphql',
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Storage-Type': storageType,
      },
      body: {
        query: query,
      },
}
    return request; 

  };