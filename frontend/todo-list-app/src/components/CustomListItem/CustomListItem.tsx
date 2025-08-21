import React from "react";
import CustomizedMenus from "../CustomizedMenus";
import {
  Avatar,
  Divider,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Typography,
} from "@mui/material";
import type { Task } from "../../models/Interfaces";

interface CustomListItemProps {
  task: Task;
  onEdit: () => void;
  onDelete: () => void;
}

const CustomListItem = ({ task, onEdit, onDelete }: CustomListItemProps) => {
  return (
    <>
      <ListItem
        alignItems="flex-start"
        sx={{
          bgcolor: task.color || "white",
          borderRadius: 5,
          mb: 1,
          height: 100,
          alignItems: "center",
        }}
      >
        <ListItemAvatar>
          <Avatar alt="Remy Sharp" src="/static/images/avatar/1.jpg" />
        </ListItemAvatar>
        <ListItemText
          primary={task.title}
          primaryTypographyProps={{
            variant: "h6",
            sx: { color: "black", fontWeight: "bold" },
          }}
          secondary={
            <React.Fragment>
              <Typography
                component="span"
                variant="body2"
                sx={{ color: "black", display: "inline" }}
              >
                {task.description || "Escribe una tarea..."}
              </Typography>
            </React.Fragment>
          }
        />
        <CustomizedMenus onEdit={onEdit} onDelete={onDelete} />
      </ListItem>
      <Divider variant="inset" component="li" />
    </>
  );
};

export default CustomListItem;
