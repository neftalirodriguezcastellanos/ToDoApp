import React from "react";
import CustomizedMenus from "../CustomizedMenus";
import {
  Checkbox,
  Divider,
  ListItem,
  ListItemText,
  Typography,
} from "@mui/material";
import type { Task } from "../../models/Interfaces";

interface CustomListItemProps {
  task: Task;
  onEdit: () => void;
  onDelete: () => void;
  onCheck: (event: React.ChangeEvent<HTMLInputElement>, id: string) => void;
}

const CustomListItem = ({
  task,
  onEdit,
  onDelete,
  onCheck,
}: CustomListItemProps) => {
  const label = { inputProps: { "aria-label": "Completar" } };
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
        <Checkbox
          {...label}
          checked={task.isCompleted}
          sx={{ "& .MuiSvgIcon-root": { fontSize: 28 } }}
          onChange={(event) => onCheck(event, task.id)}
        />
        <ListItemText
          primary={task.title}
          primaryTypographyProps={{
            variant: "h6",
            sx: {
              color: "black",
              fontWeight: "bold",
              textDecoration: task.isCompleted ? "line-through" : "none",
            },
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
              <br />
              <Typography
                component="span"
                variant="caption"
                sx={{ color: "black", display: "inline" }}
              >
                {task.dueDate
                  ? `Vence hasta: ${task.dueDate.toString().split("T")[0]}`
                  : ""}
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
