# Git

## tldr;

```
git reset --hard
git clean -fd
git pull
```


# Discarding Local Changes

If you are sure that you don't need them anymore, you can discard your local changes completely:

`git reset --hard`

If you also have untracked / new files, you will have to use the "git clean" command to get rid of these, too:

`git clean -fd`
`git pull`

-f force untracked files
-d force untracked directories
