import React from "react";
import { IUser } from "../types/IUser";

interface IUserCardProps {
  user: IUser;
}

const UserCard = (props: IUserCardProps) => (
  <div>
    <br />
    <div>UserId: {props.user.id}</div>
    <div>FirstName: {props.user.firstName}</div>
    <div>LastName: {props.user.lastName}</div>
    <div>Email: {props.user.login}</div>
  </div>
);

export default UserCard;
