import React from "react";
import { userAPI } from "../redux/services/userApi";
import UserCard from "./UserCard";
import UserCreateForm from "./UserCreateForm";

const UserContainer = () => {
  const { data: users } = userAPI.useGetUsersQuery();

  return (
    <>
      <UserCreateForm />
      {!users || users.length === 0 ? (
        <h1>Список пользователей пуст</h1>
      ) : (
        users.map((user) => <UserCard key={user.id} user={user} />)
      )}
    </>
  );
};

export default UserContainer;
